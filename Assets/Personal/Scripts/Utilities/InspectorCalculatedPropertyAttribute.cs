// using System;
// using UnityEngine;

// namespace JohnCon.Utilities.Editor
// {
// 	/// <summary>
// 	/// Marks a property to be automatically calculated and updated in the Unity Inspector at specified time intervals.
// 	/// </summary>
// 	[AttributeUsage(AttributeTargets.Property)]
// 	public sealed class InspectorCalculatedPropertyAttribute : PropertyAttribute
// 	{
// 		/// <summary>
// 		/// Delay between refreshes in seconds
// 		/// </summary>
// 		public float RefreshRate { get; private set; }

// 		/// <summary>
// 		/// If <see langword="true" />, only displays the property in the inspector while in play mode
// 		/// </summary>
// 		public bool PlayModeOnly { get; private set; }

// 		/// <param name="refreshRate">Delay between refreshes in seconds</param>
// 		/// <param name="playModeOnly">If <see langword="true" />, only displays the property in the inspector while in play mode</param>
// 		public InspectorCalculatedPropertyAttribute(float refreshRate = 0.1f, bool playModeOnly = true)
// 		{
// 			RefreshRate = refreshRate;
// 			PlayModeOnly = playModeOnly;
// 		}
// 	}
// }
