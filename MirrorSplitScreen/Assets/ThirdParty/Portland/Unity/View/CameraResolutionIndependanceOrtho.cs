using System;

using UnityEngine;

namespace Maximum.Unity.View.Camera
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class CameraResolutionIndependanceOrtho : MonoBehaviour
	{
		[SerializeField] private UnityEngine.Camera _camera;
		[SerializeField] private bool _maintainWidth = true;

		[Range(-1, 1)]
		[SerializeField] private int _adaptPosition;

		private float _defaultWeidth, _defaultHeight;

		private Vector3 _cameraPos;

		void Start()
		{

			Debug.Log(UnityEngine.Camera.main.aspect);

			Debug.Log(UnityEngine.Camera.main.orthographicSize);

			_cameraPos = _camera.transform.position;
			_defaultHeight = _camera.orthographicSize;
			_defaultWeidth = _camera.orthographicSize * _camera.aspect;
		}

		void Update()
		{
			if (_maintainWidth)
			{
				_camera.orthographicSize = _defaultWeidth / _camera.aspect;

				_camera.transform.position = new Vector3(_cameraPos.x, _adaptPosition * (_defaultHeight - _camera.orthographicSize), _cameraPos.z);
			}
			else
			{
				_camera.transform.position = new Vector3(_adaptPosition * (_defaultWeidth - _camera.orthographicSize * _camera.aspect), _cameraPos.y, _cameraPos.z);
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
