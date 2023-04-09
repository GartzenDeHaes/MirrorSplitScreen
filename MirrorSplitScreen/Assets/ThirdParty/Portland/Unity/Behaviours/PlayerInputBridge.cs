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

using UnityEngine;
using UnityEngine.InputSystem;

namespace Portland.Unity
{
	/// <summary>
	/// Used to route input from Player to global UI such as pause and map.
	/// </summary>
	public enum MapInputType
	{
		MouseDelta,
		MousePostion,
		MouseScroll,
		ZoomIn,
		ZoomOut,
		SetWaypoint,
		FocusPlayer,
		Teleport,
		Close
	}

	/// <summary>
	/// Receives input from PlayerInput and routes it to PlayerContext.
	/// TODO: should probably use C# events from PlayerInput instead of SendMessage
	/// </summary>
	public sealed class PlayerInputBridge : MonoBehaviour
	{
		[SerializeField] PlayerContext _playerContext;

		bool _zoomToggle;

		public void OnMove(InputValue value)
		{
			//Debug.Log("OnMove");
			if (_zoomToggle)
			{
				_playerContext.MoveDelta = Vector2.zero;
				_playerContext.ScrollDelta = value.Get<Vector2>();
			}
			else
			{
				_playerContext.ScrollDelta = Vector2.zero;
				_playerContext.MoveDelta = value.Get<Vector2>();
			}
		}

		public void OnZoomToggle()
		{
			_zoomToggle = !_zoomToggle;
		}

		public void OnLook(InputValue value)
		{
			//Debug.Log($"OnLook {value.Get<Vector2>()}");
			_playerContext.LookDelta = value.Get<Vector2>();
		}

		public void OnLockTarget()
		{
			_playerContext.IsLockPressed = !_playerContext.IsLockPressed;
		}

		/// <summary>
		/// Jump, Dodge, Climb
		/// </summary>
		public void OnJump()
		{
			_playerContext.IsJumpPressed = !_playerContext.IsJumpPressed;
		}

		public void OnFire()
		{
			_playerContext.IsFirePressed = !_playerContext.IsFirePressed;
		}

		public void OnUse()
		{
			_playerContext.OnUse?.Invoke();
		}

		public void OnSprint()
		{
			_playerContext.IsSprintPressed = !_playerContext.IsSprintPressed;
		}

		public void OnWalk()
		{
			_playerContext.IsWalkPressed = !_playerContext.IsWalkPressed;
		}

		public void OnScroll(InputValue value)
		{
			_playerContext.ScrollDelta = value.Get<Vector2>();
		}

		public void OnNextWeapon()
		{
			_playerContext.OnNextWeapon?.Invoke();
		}

		public void OnPrevWeapon()
		{
			_playerContext.OnPrevWeapon?.Invoke();
		}

		public void OnReload()
		{
			_playerContext.OnReload?.Invoke();
		}

		public void OnShove()
		{
			_playerContext.OnShove?.Invoke();
		}

		public void OnNavigationIn()
		{
			//MiniMap.Instance.DoInput(true, false);
			_playerContext.IsMiniMapZoomInPressed = !_playerContext.IsMiniMapZoomInPressed;
		}

		public void OnNavigationOut()
		{
			//MiniMap.Instance.DoInput(false, true);
			_playerContext.IsMiniMapZoomOutPressed = !_playerContext.IsMiniMapZoomOutPressed;
		}

		/// <summary>
		/// Crouch
		/// </summary>
		public void OnCover()
		{
			_playerContext.OnCover?.Invoke();
		}

		public void OnTelephone()
		{
			_playerContext.OnTelephone?.Invoke();
		}

		public void OnChangeCamera()
		{
			_playerContext.OnChangeCamera?.Invoke();
		}

		public void OnPause()
		{
			Context.Bus.Publish(Context.MSG_PRESS_PAUSE);
		}

		public void OnChangeSkin()
		{
			_playerContext.OnDebugChangeSkin?.Invoke();
		}

		public void OnSpawnCar()
		{
			_playerContext.OnDebugSpawnCar?.Invoke();
		}

		public void OnSteer(InputValue value)
		{
			//Debug.Log($"STEER: {value.Get<float>()}");
			_playerContext.MoveDelta = new Vector2(value.Get<float>(), _playerContext.MoveDelta.y);
		}

		public void OnAccelerate(InputValue value)
		{
			//Debug.Log($"ACC: {value.Get<float>()}");
			_playerContext.MoveDelta = new Vector2(_playerContext.MoveDelta.x, value.Get<float>());
			//_playerContext.MoveDelta = new Vector2(0f, value.Get<float>());
		}

		public void OnReverse(InputValue value)
		{
			if (_playerContext.MoveDelta.y <= 0.05f)
			{
				Debug.Log($"OnReverse {value.Get<float>()}");
				_playerContext.MoveDelta = new Vector2(_playerContext.MoveDelta.x, -value.Get<float>());
			}
		}

		public void OnSwitchCamera()
		{
			_playerContext.OnChangeCamera?.Invoke();
		}

		public void OnExitCar()
		{
			_playerContext.OnUse?.Invoke();
		}

		public void OnNextRadio()
		{
			// TODO
			_playerContext.OnRadioNext?.Invoke();
			_playerContext.OnNextWeapon?.Invoke();
		}

		public void OnPrevRadio()
		{
			// TODO
			_playerContext.OnRadioPrev?.Invoke();
			_playerContext.OnPrevWeapon?.Invoke();
		}

		public void OnCycleWeapon()
		{
			_playerContext.OnNextWeapon?.Invoke();
		}

		public void OnHandBreakToggle()
		{
			_playerContext.IsHandBreakPressed = !_playerContext.IsHandBreakPressed;
		}

		public void OnBreak(InputValue value)
		{
			_playerContext.Breaking = value.Get<float>();
		}

		bool _mapToggle;

		public void OnMap()
		{
			Context.Bus.Publish(Context.MSG_PRESS_MAP);
		}

		public void OnMapPointer(InputValue value)
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.MouseDelta, value.Get<Vector2>());
		}

		public void OnMapZoomIn()
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.ZoomIn);
		}

		public void OnMapZoomOut()
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.ZoomOut);
		}

		public void OnMapClose()
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.Close);
		}

		public void OnMapWaypoint()
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.SetWaypoint);
		}

		public void OnMapFocusOnPlayer()
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.FocusPlayer);
		}

		public void OnMapTeleportToWaypoint()
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.Teleport);
		}

		public void OnMapPosition(InputValue value)
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.MousePostion, value.Get<Vector2>());
		}

		public void OnMapScrollInOut(InputValue value)
		{
			Context.Bus.Publish(Context.MSG_MAP_INPUT, (int)MapInputType.MouseScroll, value.Get<float>());
		}

		void OnEnable()
		{
			// Changes to make the unity device simulator work
#if UNITY_2019_4
			if (Application.isMobilePlatform)
			{
				enabled = false;
			}
#else
			if (UnityEngine.Device.Application.isMobilePlatform)
			{
				enabled = false;
			}
#endif
			_playerContext.DoChangeInputMap_Pedestrian += () => {
				Debug.Log("INPUT: TPS");
				GetComponent<PlayerInput>().SwitchCurrentActionMap("TPS");
			};

			_playerContext.DoChangeInputMap_Vehicle += () => {
				Debug.Log("INPUT: CAR");
				GetComponent<PlayerInput>().SwitchCurrentActionMap("CAR");
			};

			_playerContext.DoChangeInputMap_Menu += () => {
				Debug.Log("INPUT: UI");
				GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
			};
		}

#if UNITY_EDITOR
		void OnValidate()
		{
			if (_playerContext == null)
			{
				_playerContext = GetComponent<PlayerContext>();
			}
		}
#endif
	}
}
