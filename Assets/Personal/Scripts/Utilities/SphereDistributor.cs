using UnityEngine;

namespace JohnCon.Utilities
{
	public static class SphereDistributor
	{
		/// <summary>
		/// Calculates and returns an epsilon value based on the number of points.
		/// This epsilon value is used to adjust the spacing between points when distributing them on a sphere,
		/// ensuring an even distribution especially at different scales. The value changes at predefined thresholds
		/// to better suit the distribution pattern as the number of points increases.
		/// </summary>
		/// <param name="n">The number of points to be distributed on the sphere.</param>
		/// <returns>A <c>float</c> representing the epsilon value for the given number of points.</returns>
		private static float GetEpsilon(int n)
		{
			float eps = 0.33f;

			if (n >= 600000)
				eps = 214;
			else if (n >= 400000)
				eps = 75;
			else if (n >= 11000)
				eps = 27;
			else if (n >= 890)
				eps = 10;
			else if (n >= 177)
				eps = 3.33f;
			else if (n >= 24)
				eps = 1.33f;

			return eps;
		}

		private static readonly float factor = 2.0f * Mathf.PI / ((1.0f + Mathf.Sqrt(5.0f)) * 0.5f);

		/// <summary>
		/// Distributes a specified number of points evenly on the surface of a unit sphere. This method uses the
		/// <see href="https://en.wikipedia.org/wiki/Golden_ratio">golden ratio</see> to space points evenly,
		/// avoiding clustering and ensuring a uniform distribution. The method is primarily used for visual effects
		/// or simulations requiring evenly spaced points on a spherical surface.
		/// </summary>
		/// <param name="n">The number of points to distribute on the sphere.</param>
		/// <returns>An array of <see cref="Vector3"/> points representing the coordinates of each point on the sphere.</returns>
		public static Vector3[] DistributePointsOnSphere(int n)
		{
			Vector3[] pts = new Vector3[n];

			float eps = GetEpsilon(n);
			float denom = n - 1.0f + 2.0f * eps;

			for (int i = 0; i < n; i++)
			{
				float theta = factor * i;
				float phi = Mathf.Acos(1.0f - 2.0f * (i + eps) / denom);

				pts[i] = new Vector3(
					Mathf.Cos(theta) * Mathf.Sin(phi),
					Mathf.Sin(theta) * Mathf.Sin(phi),
					Mathf.Cos(phi)
				);
			}

			return pts;
		}
	}
}
