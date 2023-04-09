// Useful for Text Meshes that should face the camera.
using UnityEngine;

namespace Mirror.Examples.SplitScreen
{
	public class FaceCamera : MonoBehaviour
	{
		public Camera PlayerCamera;

		void LateUpdate()
		{
			if (PlayerCamera != null)
			{
				transform.forward = PlayerCamera.transform.forward;
			}
		}
	}
}
