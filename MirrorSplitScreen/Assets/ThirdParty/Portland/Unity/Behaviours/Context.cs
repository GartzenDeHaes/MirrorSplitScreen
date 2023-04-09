using Portland.ComponentModel;

using UnityEngine;

namespace Portland.Unity
{
	public enum NetworkLifecycleEventType
	{
		ClientStart,
		ClientStop,
	}

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

		public static readonly String10 MSG_NET_SPAWNED = new String10("NET_SPN");
		public static readonly String10 MSG_NET_DESPAWNED = new String10("NET_UNSPN");
		public static readonly String10 MSG_PLAYER_SPAWN_LOCAL = new String10("DOSPAWN");
		public static readonly String10 MSG_PLAYER_LEFT_LOCAL = new String10("LOSTINPUT");
		//public static readonly String10 MSG_NET_CHANGE = new String10("NET_CHG");

		public static Context Inst;

		public static readonly EventBus_NoAlloc Bus = new EventBus_NoAlloc();

		//public Action<LocalNetworkPlayer> DoRegisterNetworkBridge;

		void Awake()
		{
			Inst = this;
		}
	}
}
