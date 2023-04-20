using Portland.ComponentModel;

using UnityEngine;

namespace Portland.Unity
{
	//public enum NetworkLifecycleEventType
	//{
	//	ClientStart,
	//	ClientStop,
	//}

	public sealed class Context : MonoBehaviour
	{
		void Update()
		{
			Bus.Poll();
		}

		public static readonly String10 MSG_PRESS_MAP = new String10("ON_MAP");
		public static readonly String10 MSG_PRESS_PAUSE = new String10("ON_PAUSE");

		public static readonly String10 MSG_MAP_INPUT = new String10("MAP_INPUT");
		public static readonly String10 MSG_MINIMAP_ZOOM = new String10("MINIMAPZ");

		/// <summary>Sent when the nework manager spawns the local player (LocalPlayerSpawner.cs). No arguments.</summary>
		public static readonly String10 MSG_NET_SPAWNED = new String10("NET_SPN");
		/// <summary>Sent when the nework manager calls OnStopLocalPlayer(). No arguments.</summary>
		public static readonly String10 MSG_NET_DESPAWNED = new String10("NET_UNSPN");
		/// <summary>Sent when a local player is setup (input is mapped to network player spawn). Data set to PlayerInput object.</summary>
		public static readonly String10 MSG_PLAYER_SPAWN_LOCAL = new String10("DOSPAWN");
		/// <summary>Sent when a local player input is lost or closed and the network player is despawned. NumArg set to the player input index.</summary>
		public static readonly String10 MSG_PLAYER_LEFT_LOCAL = new String10("LOSTINPUT");
		//public static readonly String10 MSG_NET_CHANGE = new String10("NET_CHG");

		public static Context Inst;

		public static readonly EventBus_NoAlloc Bus = new EventBus_NoAlloc();

		void Awake()
		{
			Inst = this;
		}
	}
}
