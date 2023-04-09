/*
Mirror Local Split Screen
https://github.com/GartzenDeHaes/

Licensed under the MIT License <http://opensource.org/licenses/MIT>.
SPDX-License-Identifier: MIT

Copyright (c) 2023 John Garrison <*@gmail.com>.

Permission is hereby  granted, free of charge, to any  person obtaining a copy
of this software and associated  documentation files (the "Software"), to deal
in the Software  without restriction, including without  limitation the rights
to  use, copy,  modify, merge,  publish, distribute,  sublicense, and/or  sell
copies  of  the Software,  and  to  permit persons  to  whom  the Software  is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE  IS PROVIDED "AS  IS", WITHOUT WARRANTY  OF ANY KIND,  EXPRESS OR
IMPLIED,  INCLUDING BUT  NOT  LIMITED TO  THE  WARRANTIES OF  MERCHANTABILITY,
FITNESS FOR  A PARTICULAR PURPOSE AND  NONINFRINGEMENT. IN NO EVENT  SHALL THE
AUTHORS  OR COPYRIGHT  HOLDERS  BE  LIABLE FOR  ANY  CLAIM,  DAMAGES OR  OTHER
LIABILITY, WHETHER IN AN ACTION OF  CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE  OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;

using Mirror;

using Portland.ComponentModel;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Portland.Unity
{
	/// <summary>
	/// LocalPlayerSpawner is spawned by NetworkManager. It's job is to spawn a local player 
	/// for each active input device.
	/// </summary>
	public sealed class LocalPlayerSpawner : NetworkBehaviour
	{
		[SerializeField] NetworkIdentity _identity;

		Dictionary<int, GameObject> _spawnedNetPlayers = new Dictionary<int, GameObject>();

		public override void OnStartLocalPlayer()
		{
			Context.Bus.Publish(Context.MSG_NET_SPAWNED);
			// Register for new local input device messages; spawn new network player objects 
			Context.Bus.Subscribe(nameof(LocalPlayerSpawner), Context.MSG_PLAYER_SPAWN_LOCAL, Client_OnLocalPlayerSpawnReq);
			// Input device disconnected; destroy network player object
			Context.Bus.Subscribe(nameof(LocalPlayerSpawner), Context.MSG_PLAYER_LEFT_LOCAL, Client_OnLocalPlayerDeSpawn);
		}

		/// <summary>
		/// Local client is shuting down
		/// </summary>
		public override void OnStopLocalPlayer()
		{
			Context.Bus.Publish(Context.MSG_NET_DESPAWNED);
			Context.Bus.UnSubscribe(nameof(LocalPlayerSpawner));

			Client_DestroyLocalPlayers();
		}

		/// <summary>
		/// A new local input device has been created; spawn a network player object
		/// </summary>
		void Client_OnLocalPlayerSpawnReq(EventMessage msg)
		{
			Client_NetSpawn((PlayerInput)msg.Data);
		}

		void Client_NetSpawn(PlayerInput pi)
		{
			if (LocalInputManager.Inst.NetplayerPrefab == null)
			{
				return;
			}

			Server_SpawnLocalPlayer(pi.playerIndex);
		}

		/// <summary>
		/// Client shutting down
		/// </summary>
		void Client_DestroyLocalPlayers()
		{
			foreach (var netp in _spawnedNetPlayers.Values)
			{
				Destroy(netp);
			}

			_spawnedNetPlayers.Clear();
		}

		/// <summary>
		/// Input device disconnected; destroy network player object
		/// </summary>
		private void Client_OnLocalPlayerDeSpawn(EventMessage msg)
		{
			int playerIndex = msg.NumArg.IntValue;
			if (_spawnedNetPlayers.ContainsKey(playerIndex))
			{
				var netp = _spawnedNetPlayers[playerIndex];
				Destroy(netp);
				_spawnedNetPlayers.Remove(playerIndex);
			}
		}

		/// <summary>
		/// Spawn the new player on the server
		/// </summary>
		/// <param name="playerIndex">The local player index on the creating client</param>
		[Command]
		void Server_SpawnLocalPlayer(int playerIndex)
		{
			NetworkIdentity player;
			var startTrans = NetworkManager.singleton.GetStartPosition();
			if (startTrans == null)
			{
				player = Instantiate(LocalInputManager.Inst.NetplayerPrefab);
			}
			else
			{
				player = Instantiate(LocalInputManager.Inst.NetplayerPrefab, startTrans.position, startTrans.rotation);
			}

			NetworkServer.Spawn(player.gameObject, base.connectionToClient);

			Client_OnServerSpawnLocalPlayer(player.gameObject, playerIndex);
		}

		/// <summary>
		/// The server has spawned a new player; setup the local player
		/// </summary>
		[ClientRpc]
		void Client_OnServerSpawnLocalPlayer(GameObject player, int playerIndex)
		{
			if (player.GetComponent<NetworkIdentity>().isOwned)
			{
				var pi = LocalInputManager.Inst.GetPlayerInput(playerIndex);
				var ctx = pi.GetComponent<PlayerContext>();
				ctx.IsLocalPlayer = true;
				player.GetComponent<INetInputSetup>().INetInputSetup_Setup(pi.camera, ctx);

				_spawnedNetPlayers.Add(playerIndex, player);
			}
		}

#if UNITY_EDITOR
		void OnValidate()
		{
			if (_identity == null)
			{
				_identity = GetComponent<NetworkIdentity>();
			}
		}
#endif
	}
}
