using System;

using UnityEngine;

namespace Portland.Unity
{
	/// <summary>
	/// Player shared state and events
	/// </summary>
	public sealed class PlayerContext : MonoBehaviour
	{
		[NonSerialized] public bool IsLocalPlayer;
		[NonSerialized] public Vector2 MoveDelta;
		[NonSerialized] public Vector2 LookDelta;
		[NonSerialized] public Vector2 ScrollDelta;

		[NonSerialized] public bool IsFirePressed;
		[NonSerialized] public bool IsLockPressed;
		[NonSerialized] public bool IsJumpPressed;
		[NonSerialized] public bool IsSprintPressed;
		[NonSerialized] public bool IsWalkPressed;
		[NonSerialized] public bool IsHandBreakPressed;
		[NonSerialized] public float Breaking;
		[NonSerialized] public bool IsMiniMapZoomInPressed;
		[NonSerialized] public bool IsMiniMapZoomOutPressed;

		[NonSerialized] public float CurrentVelocity;

		public Action OnReload;
		public Action OnShove;
		public Action OnNextWeapon;
		public Action OnPrevWeapon;
		public Action OnUse;
		public Action OnCover;
		public Action OnTelephone;
		public Action OnChangeCamera;
		public Action OnRadioNext;
		public Action OnRadioPrev;
		public Action OnFly;

		public Action OnDebugChangeSkin;
		public Action OnDebugSpawnCar;

		public Action DoChangeInputMap_Pedestrian;
		public Action DoChangeInputMap_Vehicle;
		public Action DoChangeInputMap_Menu;
	}
}
