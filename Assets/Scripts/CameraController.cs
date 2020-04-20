using Cinemachine;
using UnityEngine;

namespace Assets.Scripts
{
	public class CameraController : MonoBehaviour
	{
		private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
		private float _shakeTimeLeft;

		[SerializeField]
		private CinemachineVirtualCamera _camera;
		[SerializeField]
		private float _amplitude;
		[SerializeField]
		private float _frequecy;

		private void Awake()
		{
			_virtualCameraNoise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		}

		public void Init(Transform player)
		{
			_camera.Follow = player;
			_camera.LookAt = player;
		}

		public void Shake(float duration)
		{
			_shakeTimeLeft = duration;
		}

		private void Update()
		{
			if (_shakeTimeLeft > 0)
			{
				_virtualCameraNoise.m_AmplitudeGain = _amplitude;
				_virtualCameraNoise.m_FrequencyGain = _frequecy;

				_shakeTimeLeft -= Time.deltaTime;
			}
			else
			{
				_virtualCameraNoise.m_AmplitudeGain = 0;
				_virtualCameraNoise.m_FrequencyGain = 0;
			}
		}
	}
}