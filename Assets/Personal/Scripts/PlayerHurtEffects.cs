using UnityEngine;
using UnityEngine.Rendering;
using Unity.FPS.Game;

namespace JohnCon.Gameplay
{
	public class PlayerHurtEffects : MonoBehaviour
	{
		[Header("Components")]
		public Health PlayerHealth;

		public Volume PlayerVolumeStart;
		public Volume PlayerVolumeEnd;

		public AudioSource HeartbeatSource;

		[Header("Configuration")]
		[Range(0.0f, 1.0f)]
		public float EffectStartHealthPercentage = 0.35f;

		[Range(0.0f, 128.0f)]
		public float EffectTransitionRate = 6.0f;

		[SerializeField, Utilities.Editor.ReadOnly]
		private float _transition = 0.0f;

		void Update()
		{
			if (PlayerHealth == null) return;
			if (PlayerVolumeStart == null) return;
			if (PlayerVolumeEnd == null) return;

			_transition = Mathf.Lerp(_transition, PlayerHealth.CurrentHealth <= (EffectStartHealthPercentage * PlayerHealth.MaxHealth) ? 1.0f : 0.0f, Time.deltaTime * EffectTransitionRate);

			if (_transition != 0.0f)
			{
				HeartbeatSource.enabled = true;
				HeartbeatSource.volume = _transition;
			}
			else
				HeartbeatSource.enabled = false;

			PlayerVolumeStart.weight = 1.0f - _transition;
			PlayerVolumeEnd.weight = _transition;

			PlayerVolumeStart.enabled = PlayerVolumeStart.weight != 0.0f;
			PlayerVolumeEnd.enabled = PlayerVolumeEnd.weight != 0.0f;
		}
	}
}
