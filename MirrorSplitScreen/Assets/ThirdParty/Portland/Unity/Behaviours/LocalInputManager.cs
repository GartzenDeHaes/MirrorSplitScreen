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
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

using Portland.ComponentModel;

using Mirror;

namespace Portland.Unity
{
	/// <summary>
	/// LocalInputManager's job is to create local input managers and pair them with
	/// network player objects when the server starts.
	/// </summary>
	[RequireComponent(typeof(PlayerInputManager))]
	public class LocalInputManager : MonoBehaviour
	{
		public static LocalInputManager Inst;

		[Tooltip("Unity's input manager, used to pair devices and manage split screen camera settings.")]
		[SerializeField] PlayerInputManager _playerInputManager;
		[Tooltip("This prefab will be created for each active input device; Must have PlayerInput, PlayerContext, and a Camera.")]
		[SerializeField] GameObject _playerContextPrefab;
		[Tooltip("This will be spawned for each active input when the client is connected.")]
		public NetworkIdentity NetplayerPrefab;

		[Tooltip("On startup, automatically add the keyboard and mouse, if present.")]
		[SerializeField] bool AutoJoinKeyboardAndMouseIfPresent = true;
		[Tooltip("Automatically add input devices when they have activity (press button to join).")]
		[SerializeField] bool AutoJoinOnInput = false;
		[Tooltip("Automatically add input devices after networking starts.")]
		[SerializeField] bool AutoJoinAfterNetwork = true;

		PlayerInput[] _playerInputs;
		int _numPlayers;

		bool _origAutoJoinOnInput;

		bool _networkStarted;

		void Awake()
		{
			Inst = this;
		}

		public PlayerInput GetPlayerInput(int playerIndex)
		{
			return _playerInputs[playerIndex];
		}

		void OnEnable()
		{
			Debug.Assert(_playerInputManager.maxPlayerCount > 0);
			Debug.Assert(_playerInputManager.maxPlayerCount < 32);

			_playerInputs = new PlayerInput[_playerInputManager.maxPlayerCount];
			_numPlayers = 0;

			//StartCoroutine(WaitForContext(() => { Context.Inst.ResetInputs(); }));

			_origAutoJoinOnInput = AutoJoinOnInput;

			_playerInputManager.playerPrefab = _playerContextPrefab;
			_playerInputManager.onPlayerJoined += OnPlayerJoined;
			_playerInputManager.onPlayerLeft += OnPlayerLeft;

			DoCloseAllButKeyboard();

			//Context.Bus.Subscribe(nameof(LocalPlayerManager), Context.MSG_NET_CHANGE, OnNetwork);
			Context.Bus.Subscribe(nameof(LocalInputManager), Context.MSG_NET_SPAWNED, OnNetworkSpawnedLocalPlayer);
			Context.Bus.Subscribe(nameof(LocalInputManager), Context.MSG_NET_DESPAWNED, OnNetworkDeSpawnedLocalPlayer);

			InputUser.onUnpairedDeviceUsed += OnUnpairedDeviceUsed;
			++InputUser.listenForUnpairedDeviceActivity;

			_networkStarted = false;

			if (AutoJoinKeyboardAndMouseIfPresent && Mouse.current != null && Keyboard.current != null)
			{
				_playerInputManager.JoinPlayer
				(
					playerIndex: _numPlayers,
					pairWithDevices: new InputDevice[] { Mouse.current, Keyboard.current }
				);
			}
		}

		void OnDisable()
		{
			Context.Bus.UnSubscribe(nameof(LocalInputManager));

			_playerInputManager.onPlayerJoined -= OnPlayerJoined;
			_playerInputManager.onPlayerLeft -= OnPlayerLeft;

			InputUser.onUnpairedDeviceUsed += OnUnpairedDeviceUsed;
			++InputUser.listenForUnpairedDeviceActivity;

			for (int i = 0; i < _numPlayers; i++)
			{
				_playerInputs[i].DeactivateInput();
				Destroy(_playerInputs[i]);
			}
			_numPlayers = 0;
			_playerInputs = null;
			_networkStarted = false;
		}

		void OnNetworkSpawnedLocalPlayer(EventMessage msg)
		{
			_networkStarted = true;

			for (int i = 0; i < _numPlayers; i++)
			{
				Context.Bus.Publish(Context.MSG_PLAYER_SPAWN_LOCAL, _playerInputs[i]);
			}
		}

		void OnNetworkDeSpawnedLocalPlayer(EventMessage msg)
		{
			DoCloseAllButKeyboard();

			_networkStarted = false;
		}

		void OnPlayerJoined(PlayerInput player)
		{
			Debug.Assert(_numPlayers == player.playerIndex);

			_playerInputs[_numPlayers++] = player;

			if (player.devices.Count == 0)
			{
				Debug.LogWarning("Player joined with no devices. Ignoring");
				return;
			}
			Debug.Log($"Successful player input join on {player.devices[0].displayName + (player.devices.Count > 1 ? "+" + player.devices[1].displayName : String.Empty)}");

			if (_networkStarted && AutoJoinAfterNetwork)
			{
				Context.Bus.Publish(Context.MSG_PLAYER_SPAWN_LOCAL, player);
			}
		}

		void OnPlayerLeft(PlayerInput player)
		{
			if (player.playerIndex == 0)
			{
				DoCloseAllButKeyboard();
			}
			else
			{
				_numPlayers--;
				if (player.playerIndex != 0)
				{
					Destroy(_playerInputs[player.playerIndex].gameObject);
					_playerInputs[player.playerIndex] = null;
				}
				Context.Bus.Publish(Context.MSG_PLAYER_LEFT_LOCAL, player.playerIndex);
			}
		}

		IEnumerator WaitForContext(Action todo)
		{
			while (Context.Inst == null)
			{
				yield return new WaitForSeconds(0.1f);
			}

			todo();
		}

		void DoCloseAllButKeyboard()
		{
			for (int i = 1; i < _numPlayers; i++)
			{
				Destroy(_playerInputs[i].gameObject);
				_playerInputs[i] = null;
			}
			
			if (_numPlayers > 0)
			{
				_numPlayers = 1;
				_playerInputs[0].camera.rect = new Rect(0, 0, 1, 1);
			}

			AutoJoinOnInput = _origAutoJoinOnInput;
		}

		void OnUnpairedDeviceUsed(InputControl ctl, InputEventPtr evtptr)
		{
			Debug.Log($"Unpaired device used {ctl.device.displayName}; layout={ctl.layout}; path={ctl.path}");

			if ((AutoJoinOnInput || (_networkStarted && AutoJoinAfterNetwork)) && Context.Inst != null && _numPlayers < _playerInputs.Length)
			{
				Debug.Log($"Joining {ctl.device.displayName}");

				_playerInputManager.JoinPlayer
				(
					playerIndex: _numPlayers,
					pairWithDevice: ctl.device
				);
			}
		}

#if UNITY_EDITOR
		void OnValidate()
		{
			if (null == _playerInputManager)
			{
				_playerInputManager = GetComponent<PlayerInputManager>();
			}
		}
#endif
	}
}
