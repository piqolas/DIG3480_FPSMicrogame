using UnityEngine;
using JohnCon.Utilities;

namespace JohnCon.Gameplay
{
	[ExecuteInEditMode]
	public class PointSphereController : MonoBehaviour
	{
		[Header("Particles")]
		public new ParticleSystem particleSystem;

		[Header("Points"), Range(16, 128)]
		public int PointCount = 64;
		public Vector3[] Points;
		public int PointIndex = 0;

		[Header("Timing")]
		public float TickLength = 0.1f;
		public float LastTick { get; private set; }

		private ParticleSystem.EmitParams emitParams = new();

		void Start()
		{
			RegeneratePoints();
			PlaceNext();
		}

		void Update()
		{
			if (Time.time - LastTick >= TickLength)
				PlaceNext();
		}

		private static readonly Quaternion rotAdjustment = Quaternion.Euler(90.0f, 0.0f, 0.0f);

		void RegeneratePoints()
		{
			Points = SphereDistributor.DistributePointsOnSphere(PointCount);

			for (int i = 0; i < Points.Length; i++)
				Points[i] = rotAdjustment * Points[i];
		}

		void PlaceNext()
		{
			if (Points.Length != PointCount)
				RegeneratePoints();

			emitParams.position = Points[PointIndex++ % PointCount];

			if (particleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World)
				emitParams.position += transform.position;

			particleSystem.Emit(emitParams, 1);

			LastTick = Time.time;
		}
	}
}
