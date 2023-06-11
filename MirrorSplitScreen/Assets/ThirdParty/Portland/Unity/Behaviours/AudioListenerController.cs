using System;

using UnityEngine;

namespace Portland.Unity
{
	[RequireComponent(typeof(AudioListener))]
	public class AudioListenerController : MonoBehaviour
	{
		public static AudioListener Inst;

		[SerializeField] AudioListener _listener;

		void OnEnable()
		{
			if (Inst == null)
			{
				Inst = _listener;
			}
			else
			{
				this.enabled = false;
			}
		}

		void OnDisable()
		{
			if (Inst == _listener)
			{
				Inst = null;
			}
		}

#if UNITY_EDITOR
		void OnValidate()
		{
			if (_listener == null)
			{
				_listener = GetComponent<AudioListener>();
			}
		}
#endif
	}
}
