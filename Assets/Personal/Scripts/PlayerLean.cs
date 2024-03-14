using UnityEngine;

namespace JohnCon.Gameplay
{
	public class PlayerLean : MonoBehaviour
	{
		[Range(1.0f, 90.0f)]
		public float LeanAngle = 5.0f; // Max lean angle

		[Range(0.1f, 32.0f)]
		public float LeanSpeed = 15.0f; // Speed of leaning

		[Range(0.0f, 1.0f)]
		public float LeanSpeedMultiplier = 0.055f;

		[Range(0.0f, 1.0f)]
		public float LeanDeadzone = 0.005f;

		public CharacterController PlayerController; // Reference to the player's Character Controller

		[SerializeField, Util.Editor.ReadOnly]
		private float _lean = 0.0f;

		void Update()
		{
			if (PlayerController == null) return;

			// Convert global velocity to local space and use local velocity's X component for strafe speed, which corresponds to left/right movement
			float strafeSpeed = transform.InverseTransformDirection(PlayerController.velocity).x;
			_lean = Mathf.Lerp(_lean, Mathf.Clamp(LeanSpeedMultiplier * strafeSpeed, -1.0f, 1.0f), Time.deltaTime * LeanSpeed);

			// Debugging to check local strafe speed
			// Debug.Log($"Current local strafe speed: {strafeSpeed}");

			if (Mathf.Abs(_lean) > LeanDeadzone) // Try to improve performance when the difference isn't noticeable
			{
				transform.localRotation = transform.localRotation * Quaternion.Euler(0.0f, 0.0f, -_lean * LeanAngle);

				// Debug.Log($"Current targetLean: {targetLean}");
				// Debug.Log($"Current angle: {transform.localRotation}");
			}
		}
	}
}
