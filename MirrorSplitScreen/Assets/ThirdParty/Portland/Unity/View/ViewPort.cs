using System;

using UnityEngine;

namespace Portland.Unity.View
{
	/// <summary>
	/// ViewPort controls the canvas and handled UI input for this player
	/// </summary>
	public sealed class ViewPort : MonoBehaviour
	{
		public enum ViewPortMode
		{
			Full,
			Left,
			Right,
			UpperLeft,
			UpperRight,
			LowerLeft,
			LowerRight
		}

		[SerializeField] Camera _camera;
		[SerializeField] RectTransform _uiRoot;
		[SerializeField] ViewPortMode _defaultViewMode = ViewPortMode.Full;

		public ViewPortMode CurrentViewMode { get; private set; }

		void Awake()
		{
			CurrentViewMode = _defaultViewMode;
		}

		void OnEnable()
		{
			SetViewMode(_defaultViewMode);
		}

		void OnDisable()
		{
		}

		public void SetViewMode(ViewPortMode mode)
		{
			CurrentViewMode = mode;

			if (_camera == null)
			{
				return;
			}

			switch (mode)
			{
				case ViewPortMode.Full:
					_camera.rect = new Rect(0f, 0f, 1f, 1f);
					_uiRoot.rect.Set(0, 0, 1280, 720);
					break;
				case ViewPortMode.Left:
					_camera.rect = new Rect(0f, 0f, 0.5f, 1f);
					_uiRoot.rect.Set(0, 0, 640, 720);
					break;
				case ViewPortMode.Right:
					_camera.rect = new Rect(0.5f, 0f, 1f, 1f);
					_uiRoot.rect.Set(320, 0, 640, 720);
					break;
				case ViewPortMode.LowerLeft:
					_camera.rect = new Rect(0f, 0f, 0.5f, 0.5f);
					_uiRoot.rect.Set(-320, -180, 640, 360);
					break;
				case ViewPortMode.LowerRight:
					_camera.rect = new Rect(0.5f, 0f, 1f, 0.5f);
					_uiRoot.rect.Set(320, -180, 640, 360);
					break;
				case ViewPortMode.UpperLeft:
					_camera.rect = new Rect(0f, 0.5f, 0.5f, 1f);
					_uiRoot.rect.Set(0, 0, 640, 360);
					break;
				case ViewPortMode.UpperRight:
					_camera.rect = new Rect(0.5f, 0.5f, 1f, 1f);
					_uiRoot.rect.Set(320, 0, 640, 360);
					break;
			}
		}
	}
}
