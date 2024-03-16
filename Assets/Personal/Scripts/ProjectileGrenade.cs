using Unity.FPS.Game;
using UnityEngine;
using JohnCon.Utilities.Extensions;

namespace JohnCon.Gameplay
{
	[RequireComponent(typeof(Rigidbody), typeof(DamageArea))]
	public class ProjectileGrenade : ProjectileBase
	{
		public float Damage = 100.0f;
		public DamageArea AreaDamage;
		public LayerMask HittableLayers = -1;

		public float Delay = 2.5f;

		public float ShootSpeed = 12.0f;
		public float AngularVelocityScale = 3.5f * Mathf.PI;

		public GameObject ExplosionVFX;
		public float ExplosionVFXLifetime = 3.0f;
		public float ExplosionVFXRotVelocityThreshold = 0.6f;

		public AudioClip[] ExplosionSFX;

		public float TimeSinceShot => Time.time - _timeShot;

		[SerializeField, Utilities.Editor.ReadOnly]
		private float _timeShot;

		private const QueryTriggerInteraction TriggerInteraction = QueryTriggerInteraction.Collide;

		void OnEnable()
		{
			((ProjectileBase)this).OnShoot += OnShoot;
		}

		new void OnShoot()
		{
			if (TryGetComponent(out Rigidbody body))
			{
				body.velocity = ShootSpeed * InitialDirection /* + InheritedMuzzleVelocity */;
				body.angularVelocity = new Vector3(Random.Range(-AngularVelocityScale, AngularVelocityScale), Random.Range(-AngularVelocityScale, AngularVelocityScale), Random.Range(-AngularVelocityScale, AngularVelocityScale));
			}

			transform.rotation = Random.rotation;

			_timeShot = Time.time;
		}

		void Update()
		{
			if (TimeSinceShot >= Delay)
				Explode();
		}

		void Explode()
		{
			if (AreaDamage)
				AreaDamage.InflictDamageInArea(Damage, transform.position, HittableLayers, TriggerInteraction, Owner);

			if (ExplosionVFX)
			{
				Quaternion rot;

				// If the grenade's moving and moving fast enough, the explosion effect should explode in that direction
				if (TryGetComponent(out Rigidbody body) && body.velocity.magnitude >= ExplosionVFXRotVelocityThreshold)
					rot = Quaternion.LookRotation(body.velocity.normalized);
				else
					rot = Quaternion.LookRotation(Vector3.up);			

				// Instance the explosion effect
				GameObject vfx = Instantiate(ExplosionVFX, transform.position, rot);

				// If ExplosionVFXLifetime > 0.0f, destroy the effect instance after that amount of time
				if (ExplosionVFXLifetime > 0.0f)
					Destroy(vfx, ExplosionVFXLifetime);
				
				if (ExplosionSFX?.Length != 0)
				{
					AudioUtility.CreateSFX(ExplosionSFX.Random(), transform.position, AudioUtility.AudioGroups.Impact, 1.0f, 4.0f);

					// Get rid of the explosion VFX's sound effect if it has one
					if (vfx.TryGetComponent(out AudioSource audioSource))
						Destroy(audioSource);
				}
			}

			Destroy(gameObject);
		}
	}
}
