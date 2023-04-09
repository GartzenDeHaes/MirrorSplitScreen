#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

namespace CFG.Runtime.Input
{
	// Using InputControlLayoutAttribute, we tell the system about the state
	// struct we created, which includes where to find all the InputControl
	// attributes that we placed on there. This is how the Input System knows
	// what controls to create and how to configure them.
	[InputControlLayout(stateType = typeof(SteamControllerHIDInputReport2))]
#if UNITY_EDITOR
	[InitializeOnLoad] // Make sure static constructor is called during startup.
#endif
	public class SteamControllerHID : InputDevice
	{
		// [InitializeOnLoad] will ensure this gets called on every domain (re)load
		// in the editor.
#if UNITY_EDITOR
		static SteamControllerHID()
		{
			// Trigger our RegisterLayout code in the editor.
			Initialize();
		}
#endif

		// In the Player, to trigger the calling of the static constructor,
		// create an empty method annotated with RuntimeInitializeOnLoadMethod.
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void Initialize() 
		{
			// This is one way to match the Device.
			//InputSystem.RegisterLayout<SteamControllerHID>(name:null,
			//    new InputDeviceMatcher()
			//        .WithInterface("HID")
			//        .WithManufacturer("Valve Software")
			//        .WithProduct("Steam Controller"));//Wireless Steam Controller
			//InputSystem.RegisterLayout<SteamControllerHID>(name: null,
			//    new InputDeviceMatcher()
			//        .WithInterface("HID")
			//        .WithManufacturer("Valve Software")
			//        .WithProduct("Valve"));//Wired Controller

			// Alternatively, you can also match by PID and VID, which is generally
			// more reliable for HIDs.
			InputSystem.RegisterLayout<SteamControllerHID>(name: "Wireless Steam Controller",
				 matches: new InputDeviceMatcher()
					  .WithInterface("HID")
					  .WithCapability("vendorId", 0x28de) // Valve Software
					  .WithCapability("productId", 0x1142)); // Wireless Steam Controller

			InputSystem.RegisterLayout<SteamControllerHID>(name: "Wired Steam Controller",
				 matches: new InputDeviceMatcher()
					  .WithInterface("HID")
					  .WithCapability("vendorId", 0x28de) // Valve Software
					  .WithCapability("productId", 0x1102)); // Wired Controller
		}

		public static SteamControllerHID current { get; private set; }
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		// When one of our custom devices is removed, we want to make sure that if
		// it is the '.current' device, we null out '.current'.
		protected override void OnRemoved()
		{
			base.OnRemoved();

			if (current == this)
				current = null;
		}
	}

	// Using InputControlLayoutAttribute, we tell the system about the state
	// struct we created, which includes where to find all the InputControl
	// attributes that we placed on there. This is how the Input System knows
	// what controls to create and how to configure them.
	[InputControlLayout(stateType = typeof(SteamControllerHIDInputReport_BT))]
#if UNITY_EDITOR
	[InitializeOnLoad] // Make sure static constructor is called during startup.
#endif
	public class SteamControllerHID_BT : InputDevice
	{
		// [InitializeOnLoad] will ensure this gets called on every domain (re)load
		// in the editor.
#if UNITY_EDITOR
		static SteamControllerHID_BT()
		{
			// Trigger our RegisterLayout code in the editor.
			Initialize();
		}
#endif

		// In the Player, to trigger the calling of the static constructor,
		// create an empty method annotated with RuntimeInitializeOnLoadMethod.
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void Initialize()
		{
			// This is one way to match the Device.
			//InputSystem.RegisterLayout<SteamControllerHID>(name:null,
			//    new InputDeviceMatcher()
			//        .WithInterface("HID")
			//        .WithManufacturer("Valve Software")
			//        .WithProduct("Steam Controller"));//Wireless Steam Controller
			//InputSystem.RegisterLayout<SteamControllerHID>(name: null,
			//    new InputDeviceMatcher()
			//        .WithInterface("HID")
			//        .WithManufacturer("Valve Software")
			//        .WithProduct("Valve"));//Wired Controller

			// Alternatively, you can also match by PID and VID, which is generally
			// more reliable for HIDs.

			InputSystem.RegisterLayout<SteamControllerHID_BT>(name: "Bluetooth Steam Controller",
				 matches: new InputDeviceMatcher()
					  //.WithInterface("HID")
					  .WithCapability("vendorId", 0x28de) // Valve Software
					  .WithCapability("productId", 0x1106)); // Wireless Steam Controller

			//InputSystem.RegisterLayout<SteamControllerHID>(name: "Wireless Steam Controller",
			//	 matches: new InputDeviceMatcher()
			//		  //.WithInterface("HID")
			//		  .WithCapability("vendorId", 0x28de) // Valve Software
			//		  .WithCapability("productId", 4358)); // Wireless Steam Controller
		}

		public static SteamControllerHID_BT current { get; private set; }
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		// When one of our custom devices is removed, we want to make sure that if
		// it is the '.current' device, we null out '.current'.
		protected override void OnRemoved()
		{
			base.OnRemoved();

			if (current == this)
				current = null;
		}
	}
}
