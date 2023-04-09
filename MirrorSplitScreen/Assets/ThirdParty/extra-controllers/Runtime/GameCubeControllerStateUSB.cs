using System.Runtime.InteropServices;

using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	public struct GameCubeControllerStateUSB : IInputStateTypeInfo
	{
		public FourCC format => new FourCC('H', 'I', 'D');

		//[FieldOffset(6)]
		//public byte moreButtons;
		//
		//[InputControl(name = "0", displayName = "0", bit = 0, format = "BIT", layout = "Button")]
		//[InputControl(name = "1", displayName = "1", bit = 1, format = "BIT", layout = "Button")]
		//[InputControl(name = "2", displayName = "2", bit = 2, format = "BIT", layout = "Button")]
		//[InputControl(name = "3", displayName = "3", bit = 3, format = "BIT", layout = "Button")]
		//[InputControl(name = "4", displayName = "4", bit = 4, format = "BIT", layout = "Button")]
		//[InputControl(name = "5", displayName = "5", bit = 5, format = "BIT", layout = "Button")]
		//[InputControl(name = "6", displayName = "6", bit = 6, format = "BIT", layout = "Button")]
		//[InputControl(name = "7", displayName = "7", bit = 7, format = "BIT", layout = "Button")]
		//[InputControl(name = "8", displayName = "8", bit = 8, format = "BIT", layout = "Button")]
		//[InputControl(name = "9", displayName = "9", bit = 9, format = "BIT", layout = "Button")]
		//[InputControl(name = "10", displayName = "10", bit = 10, format = "BIT", layout = "Button")]
		//[InputControl(name = "11", displayName = "11", bit = 11, format = "BIT", layout = "Button")]
		//[InputControl(name = "12", displayName = "12", bit = 12, format = "BIT", layout = "Button")]
		//[InputControl(name = "13", displayName = "13", bit = 13, format = "BIT", layout = "Button")]
		//[InputControl(name = "14", displayName = "14", bit = 14, format = "BIT", layout = "Button")]
		//[InputControl(name = "15", displayName = "15", bit = 15, format = "BIT", layout = "Button")]
		//[FieldOffset(25)]
		//public ushort bits;

		//[FieldOffset(6)]
		//public byte dpad;

		[InputControl(name = "leftStick", usage = "Primary2DMotion", processors = "stickDeadzone", displayName = "Left Joystick", layout = "Stick", format = "VCB2")]
		[InputControl(name = "leftStick/x", displayName = "X", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0.05,clampMin=0,clampMax=1,normalizeMax=0.95,normalizeZero=0.5")]
		[InputControl(name = "leftStick/left", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/right", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "leftStick/y", displayName = "Y", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0.05,clampMin=0,clampMax=1,normalizeMax=0.95,normalizeZero=0.5,invert")]
		[InputControl(name = "leftStick/up", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/down", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		[FieldOffset(1)]
		public short joystickX;

		[InputControl(name = "controlStick", usage = "Secondary2DMotion", processors = "stickDeadzone", displayName = "Control Stick", layout = "Stick", format = "VCB2")]
		[InputControl(name = "controlStick/x", displayName = "X", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0.05,clampMin=0,clampMax=1,normalizeMax=0.95,normalizeZero=0.5")]
		[InputControl(name = "controlStick/left", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "controlStick/right", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "controlStick/y", displayName = "Y", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0.05,clampMin=0,clampMax=1,normalizeMax=0.95,normalizeZero=0.5,invert")]
		[InputControl(name = "controlStick/up", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "controlStick/down", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		[FieldOffset(3)]
		public short cStickX;

		[InputControl(name = "dpad", displayName = "DPAD", format = "BIT", layout = "Dpad", bit = 0, sizeInBits = 4)]
		[InputControl(name = "dpad/up", displayName = "Up", bit = 0, format = "BIT", layout = "Button")]
		[InputControl(name = "dpad/left", displayName = "Left", bit = 3, format = "BIT", layout = "Button")]
		[InputControl(name = "dpad/down", displayName = "Down", bit = 2, format = "BIT", layout = "Button")]
		[InputControl(name = "dpad/right", displayName = "Right", bit = 1, format = "BIT", layout = "Button")]
		[InputControl(name = "xButton", displayName = "X", bit = 4, format = "BIT", layout = "Button")]
		[InputControl(name = "yButton", displayName = "Y", bit = 5, format = "BIT", layout = "Button")]
		[InputControl(name = "aButton", displayName = "A", bit = 6, format = "BIT", layout = "Button")]
		[InputControl(name = "bButton", displayName = "B", bit = 7, format = "BIT", layout = "Button")]
		[FieldOffset(6)]
		public byte faceButtons;

		[InputControl(name = "leftTriggerButton", displayName = "L Button", bit = 0, format = "BIT", layout = "Button")]
		[InputControl(name = "rightTriggerButton", displayName = "R Button", bit = 1, format = "BIT", layout = "Button")]
		[InputControl(name = "zButton", displayName = "Z", bit = 2, format = "BIT", layout = "Button")]
		[InputControl(name = "startButton", displayName = "Start", bit = 5, format = "BIT", layout = "Button")]
		[FieldOffset(7)]
		public byte startButton;

		//[FieldOffset(6)]
		//public byte cStickY;

		[InputControl(name = "leftTrigger", format ="BYTE", usage = "SecondaryTrigger", displayName = "L Trigger", layout = "Axis", shortDisplayName = "LB")]
		[FieldOffset(8)]
		public byte leftTrigger;

		[InputControl(name = "rightTrigger", format ="BYTE", usage = "SecondaryTrigger", displayName = "R Trigger", layout = "Axis", shortDisplayName = "RB")]
		[FieldOffset(9)]
		public byte rightTrigger;

		public enum Button1
		{
			X = 0,
			A = 1,
			B = 2,
			Y = 3,
			LButton = 4,
			RButton = 5,
			ZButton = 7
		}
	}
}
