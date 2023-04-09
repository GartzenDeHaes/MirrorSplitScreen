using System.Runtime.InteropServices;

using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace CFG.Runtime.Input
{
	// BT firmware update seem to change the layout??
	[StructLayout(LayoutKind.Explicit, Size = 60)]
	public struct SteamControllerHIDInputReport_BT : IInputStateTypeInfo
	{
		public FourCC format => new FourCC('H', 'I', 'D');

		// left stick 10-12

		[InputControl(name = "0", displayName = "0", bit = 0, format = "BIT", layout = "Button")]
		[InputControl(name = "1", displayName = "1", bit = 1, format = "BIT", layout = "Button")]
		[InputControl(name = "2", displayName = "2", bit = 2, format = "BIT", layout = "Button")]
		[InputControl(name = "3", displayName = "3", bit = 3, format = "BIT", layout = "Button")]
		[InputControl(name = "4", displayName = "4", bit = 4, format = "BIT", layout = "Button")]
		[InputControl(name = "5", displayName = "5", bit = 5, format = "BIT", layout = "Button")]
		[InputControl(name = "6", displayName = "6", bit = 6, format = "BIT", layout = "Button")]
		[InputControl(name = "7", displayName = "7", bit = 7, format = "BIT", layout = "Button")]
		[InputControl(name = "8", displayName = "8", bit = 8, format = "BIT", layout = "Button")]
		[InputControl(name = "9", displayName = "9", bit = 9, format = "BIT", layout = "Button")]
		[InputControl(name = "10", displayName = "10", bit = 10, format = "BIT", layout = "Button")]
		[InputControl(name = "11", displayName = "11", bit = 11, format = "BIT", layout = "Button")]
		[InputControl(name = "12", displayName = "12", bit = 12, format = "BIT", layout = "Button")]
		[InputControl(name = "13", displayName = "13", bit = 13, format = "BIT", layout = "Button")]
		[InputControl(name = "14", displayName = "14", bit = 14, format = "BIT", layout = "Button")]
		[InputControl(name = "15", displayName = "15", bit = 15, format = "BIT", layout = "Button")]
		[FieldOffset(2)]
		public ushort bits;

		//FieldOffset 5-8 sequence number
		//[InputControl(name = "rightTriggerButton", layout = "Button", bit = 0)]
		//[InputControl(name = "leftTriggerButton", layout = "Button", bit = 1)]
		//[InputControl(name = "rightShoulder", layout = "Button", bit = 2)]
		//[InputControl(name = "leftShoulder", layout = "Button", bit = 3)]
		//[InputControl(name = "buttonNorth", displayName = "Y", layout = "Button", bit = 4)]
		//[InputControl(name = "buttonEast", displayName = "B", layout = "Button", bit = 5)]
		//[InputControl(name = "buttonWest", displayName = "X", layout = "Button", bit = 6)]
		//[InputControl(name = "buttonSouth", displayName = "A", layout = "Button", bit = 7)]
		//[FieldOffset(8)] public byte buttonsSet1;
		//[InputControl(name = "leftPad", layout = "Stick", format = "BYTE")]
		//[InputControl(name = "leftPad/Up", layout = "Button", bit = 0)]
		//[InputControl(name = "leftPad/Right", layout = "Button", bit = 1)]
		//[InputControl(name = "leftPad/Left", layout = "Button", bit = 2)]
		//[InputControl(name = "leftPad/Down", layout = "Button", bit = 3)]
		//[InputControl(name = "select", displayName = "Back", layout = "Button", bit = 4)]
		//[InputControl(name = "systemButton", displayName = "Steam Home Button", layout = "Button", bit = 5)]
		//[InputControl(name = "start", displayName = "Start", layout = "Button", bit = 6)]
		//[InputControl(name = "leftGrip", layout = "Button", bit = 7)]
		//[FieldOffset(9)] public byte buttonsSet2;
		//[InputControl(name = "righPad", layout = "Stick", format = "BYTE")]
		//[InputControl(name = "rightGrip", layout = "Button", bit = 0)]
		//[InputControl(name = "leftPad/Clicked", layout = "Button", bit = 1)]
		//[InputControl(name = "righPad/Clicked", layout = "Button", bit = 2)]
		//[InputControl(name = "leftDpadTouched", displayName = "Left Dpad Touched", layout = "Button", bit = 3)]
		//[InputControl(name = "rightDpadTouched", displayName = "Right Dpad Touched", layout = "Button", bit = 4)]
		////offset 5 unknown
		//[InputControl(name = "joystick_clicked", layout = "Button", bit = 6)]
		//[InputControl(name = "lpad_and_joy", layout = "Button", bit = 7)]
		//[FieldOffset(10)] public byte buttonsSet3;

		//[InputControl(name = "leftTrigger", layout = "Button", format = "BYTE")]
		//[FieldOffset(11)] public byte leftTrigger;
		//[InputControl(name = "rightTrigger", layout = "Button", format = "BYTE")]
		//[FieldOffset(12)] public byte q1;
		////FieldOffset 14-16 always 0

		//[InputControl(name = "leftStick", usage = "Primary2DMotion", processors = "stickDeadzone", displayName = "Left Joystick", layout = "Stick", format = "VCB2")]
		//[InputControl(name = "leftStick/x", displayName = "X", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0.05,clampMin=0,clampMax=1,normalizeMax=0.95,normalizeZero=0.5")]
		//[InputControl(name = "leftStick/left", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		//[InputControl(name = "leftStick/right", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		//[InputControl(name = "leftStick/y", displayName = "Y", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0.05,clampMin=0,clampMax=1,normalizeMax=0.95,normalizeZero=0.5,invert")]
		//[InputControl(name = "leftStick/up", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		//[InputControl(name = "leftStick/down", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		//[FieldOffset(9)] public short leftStickX;
		//[FieldOffset(11)] public short leftStickY;

		[InputControl(name = "leftStick", usage = "Primary2DMotion", processors = "stickDeadzone", displayName = "Left Joystick", layout = "Stick")]
		[InputControl(name = "leftStick/x", displayName = "X", offset = 0, format = "BYTE")]
		[InputControl(name = "leftStick/left", offset = 0, format = "BYTE", parameters = "invert")]
		[InputControl(name = "leftStick/right", offset = 0, format = "BYTE", parameters = "")]
		[InputControl(name = "leftStick/y", displayName = "Y", offset = 1, format = "BYTE", parameters = "invert")]
		[InputControl(name = "leftStick/up", offset = 1, format = "BYTE", parameters = "invert")]
		[InputControl(name = "leftStick/down", offset = 1, format = "BYTE", parameters = "")]
		[FieldOffset(10)] public short leftStickX;
		[FieldOffset(12)] public short leftStickY;




		//[InputControl(name = "leftStickX", layout = "Button", format = "SHRT", parameters = "normalize,normalizeMin=-0.9999695,normalizeMax=0.9999695,normalizeZero=0")]
		//[FieldOffset(10)] public short leftStickX;
		//[InputControl(name = "leftStickY", layout = "Button", format = "SHRT",
		//	 parameters = "normalize,normalizeMin=-0.9999695,normalizeMax=0.9999695,normalizeZero=0")]
		//[FieldOffset(12)] public short leftStickY;

		//[InputControl(name = "leftStick", layout = "Stick", format = "VC2S")]
		//[InputControl(name = "leftStick/x", offset = 0, layout = "Axis", format = "SHRT",
		//		 parameters = "normalize,normalizeMin=-0.9999695,normalizeMax=0.9999695,normalizeZero=0")]
		//[InputControl(name = "leftStick/left", offset = 0,
		//		 parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=-0.5,clamp,clampMin=-1,clampMax=0,invert")]
		//[InputControl(name = "leftStick/right", offset = 0,
		//		 parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0,clampMax=1")]
		//[InputControl(name = "leftStick/y", offset = 2, layout = "Axis", format = "SHRT",
		//		 parameters = "normalize,normalizeMin=-0.9999695,normalizeMax=0.9999695,normalizeZero=0")]
		//[InputControl(name = "leftStick/up", offset = 2,
		//		 parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0,clampMax=1,invert=false")]
		//[InputControl(name = "leftStick/down", offset = 2,
		//		 parameters = "normalize,normalizeMin=-1,normalizeMax=1,normalizeZero=-0.5,clamp,clampMin=-1,clampMax=0,invert")]
		//[FieldOffset(17)] public short leftStickX;
		//[FieldOffset(19)] public short leftStickY;
	}

	// We receive data as raw HID input reports. This struct
	// describes the raw binary format of such a report.
	[StructLayout(LayoutKind.Explicit, Size = 60)]
	public struct SteamControllerHIDInputReport2 : IInputStateTypeInfo
	{
		public FourCC format => new FourCC('H', 'I', 'D');

		//FieldOffset 5-8 sequence number
		[InputControl(name = "rightTriggerButton", layout = "Button", bit = 0)]
		[InputControl(name = "leftTriggerButton", layout = "Button", bit = 1)]
		[InputControl(name = "rightShoulder", layout = "Button", bit = 2)]
		[InputControl(name = "leftShoulder", layout = "Button", bit = 3)]
		[InputControl(name = "buttonNorth", displayName = "Y", layout = "Button", bit = 4)]
		[InputControl(name = "buttonEast", displayName = "B", layout = "Button", bit = 5)]
		[InputControl(name = "buttonWest", displayName = "X", layout = "Button", bit = 6)]
		[InputControl(name = "buttonSouth", displayName = "A", layout = "Button", bit = 7)]
		[FieldOffset(9)] public byte buttonsSet1;
		[InputControl(name = "leftPad", layout = "Stick", format = "BYTE")]
		[InputControl(name = "leftPad/Up", layout = "Button", bit = 0)]
		[InputControl(name = "leftPad/Right", layout = "Button", bit = 1)]
		[InputControl(name = "leftPad/Left", layout = "Button", bit = 2)]
		[InputControl(name = "leftPad/Down", layout = "Button", bit = 3)]
		[InputControl(name = "select", displayName = "Back", layout = "Button", bit = 4)]
		[InputControl(name = "systemButton", displayName = "Steam Home Button", layout = "Button", bit = 5)]
		[InputControl(name = "start", displayName = "Start", layout = "Button", bit = 6)]
		[InputControl(name = "leftGrip", layout = "Button", bit = 7)]
		[FieldOffset(10)] public byte buttonsSet2;
		[InputControl(name = "righPad", layout = "Stick", format = "BYTE")]
		[InputControl(name = "rightGrip", layout = "Button", bit = 0)]
		[InputControl(name = "leftPad/Clicked", layout = "Button", bit = 1)]
		[InputControl(name = "righPad/Clicked", layout = "Button", bit = 2)]
		[InputControl(name = "leftDpadTouched", displayName = "Left Dpad Touched", layout = "Button", bit = 3)]
		[InputControl(name = "rightDpadTouched", displayName = "Right Dpad Touched", layout = "Button", bit = 4)]
		//offset 5 unknown
		[InputControl(name = "joystick_clicked", layout = "Button", bit = 6)]
		[InputControl(name = "lpad_and_joy", layout = "Button", bit = 7)]
		[FieldOffset(11)] public byte buttonsSet3;


		[InputControl(name = "leftTrigger", layout = "Button", format = "BYTE")]
		[FieldOffset(12)] public byte leftTrigger;
		[InputControl(name = "rightTrigger", layout = "Button", format = "BYTE")]
		[FieldOffset(13)] public byte q1;
		//FieldOffset 14-16 always 0
		[InputControl(name = "leftStickX0", layout = "Button", format = "BYTE")]
		[FieldOffset(17)] public byte leftStickX0;
		[InputControl(name = "leftStickX1", layout = "Button", format = "BYTE")]
		[FieldOffset(18)] public byte leftStickX1;
		[InputControl(name = "leftStickY0", layout = "Button", format = "BYTE")]
		[FieldOffset(19)] public byte leftStickY0;
		[InputControl(name = "leftStickY1", layout = "Button", format = "BYTE")]
		[FieldOffset(20)] public byte leftStickY1;

		[InputControl(name = "leftStickX", layout = "Button", format = "SHRT",
						parameters = "normalize,normalizeMin=-0.9999695,normalizeMax=0.9999695,normalizeZero=0")]
		[FieldOffset(17)] public short leftStickX;
		[InputControl(name = "leftStickY", layout = "Button", format = "SHRT",
			 parameters = "normalize,normalizeMin=-0.9999695,normalizeMax=0.9999695,normalizeZero=0")]
		[FieldOffset(19)] public short leftStickY;

	}
}
