using System;

using UnityEngine;

namespace Maximum.Unity.View.Camera
{
	/**
	 * This class attempts to force VERT- Field of view scaling.
	 * By default, Unity uses the HOR+ technique.
	 * 
	 * http://en.wikipedia.org/wiki/Field_of_view_in_video_games#Scaling_methods
	 */

	[ExecuteInEditMode]
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class CameraResolutionIndependanceFOV : MonoBehaviour
	{
		[SerializeField] private UnityEngine.Camera _camera;

		[SerializeField] private float _designTimeVerticalFieldOfView = 60;
		[SerializeField] private int _designTimeWidth = 1280;
		[SerializeField] private int _designTimeHeight = 720;

		private float _hFOVInRads;
		private int _prevWidth;
		private int _prevHeight;

		void Start()
		{
			_prevWidth = _designTimeWidth;
			_prevHeight = _designTimeHeight;

			float aspectRatio = (float)_designTimeWidth / (float)_designTimeHeight;
			float vFOVInRads = _designTimeVerticalFieldOfView * Mathf.Deg2Rad;
			_hFOVInRads = 2f * Mathf.Atan(Mathf.Tan(vFOVInRads / 2f) * aspectRatio);
		}

		void Update()
		{
			if (Screen.width != _prevWidth || Screen.height != _prevHeight)
			{
				float aspectRatio = (float)Screen.width / (float)Screen.height;

				float vFOVInRads = 2f * Mathf.Atan(Mathf.Tan(_hFOVInRads / 2f) / aspectRatio);

				//Debug.Log("Screen resolution change. Recomputing aspect ratio (" + aspectRatio + ") and field of view (" + vFOVInRads * Mathf.Rad2Deg + ")");

				_camera.fieldOfView = vFOVInRads * Mathf.Rad2Deg;
			}
		}

#if UNITY_EDITOR
	void OnValidate()
		{
			if (_camera == null)
			{
				_camera = GetComponent<UnityEngine.Camera>();
			}
		}
#endif
	}
}
