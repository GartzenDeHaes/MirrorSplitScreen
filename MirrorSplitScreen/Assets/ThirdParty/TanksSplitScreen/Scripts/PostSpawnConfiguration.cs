using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Portland.Unity;

using UnityEngine;

namespace Mirror.Examples.SplitScreen
{
	public class PostSpawnConfiguration : MonoBehaviour, INetInputSetup
	{
		[SerializeField] Tank TankComponent;
		[SerializeField] GameObject CameraMountPoint;
		[SerializeField] FaceCamera HealthBar;

		Camera _camera;
		Transform _origCameraParent;
		Vector3 _origCameraPosition;
		Quaternion _origCameraRotation;

		void OnEnable()
		{
			//TankComponent.enabled = false;
		}

		void OnDisable()
		{
			if (_camera != null)
			{
				_camera.transform.SetParent(_origCameraParent, false);
				_camera.transform.position = _origCameraPosition;
				_camera.transform.rotation = _origCameraRotation;
			}
		}

		public void INetInputSetup_Setup(Camera camera, PlayerContext context)
		{
			//TankComponent._camera = camera;
			TankComponent.PlayerContext = context;
			HealthBar.PlayerCamera = camera;
			//CameraMountPoint.PlayerContext = context;

			_camera = camera;
			_origCameraParent = camera.transform.parent;
			_origCameraPosition = camera.transform.position;
			_origCameraRotation = camera.transform.rotation;

			camera.transform.position = Vector3.zero;
			camera.transform.rotation = Quaternion.identity;
			camera.transform.SetParent(CameraMountPoint.transform, false);

			TankComponent.enabled = true;
		}
	}
}
