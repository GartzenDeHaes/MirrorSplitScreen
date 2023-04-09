using UnityEngine;

namespace Portland.Unity
{
	public class SimpleFollowCamera : MonoBehaviour
	{
		[SerializeField] Transform _target;
		[SerializeField] float _smoothSpeed = 0.125f;
		[SerializeField] Vector3 _offset;

		void Update()
		{
			Vector3 desiredPosition = _target.position + new Vector3(_target.forward.x * _offset.x, _offset.y, _target.forward.z * _offset.z);
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
			transform.position = smoothedPosition;

			transform.LookAt(_target);
		}
	}
}
