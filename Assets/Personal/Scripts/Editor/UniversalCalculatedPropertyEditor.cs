// using System.Collections.Generic;
// using System.Reflection;
// using UnityEditor;

// namespace JohnCon.Utilities.Editor
// {
// 	[CustomEditor(typeof(UnityEngine.Object), true, isFallback = true)]
// 	[CanEditMultipleObjects]
// 	public sealed class UniversalCalculatedPropertyDrawer : UnityEditor.Editor
// 	{
// 		private const BindingFlags _BindingFlags = BindingFlags.Public | BindingFlags.Instance;
// 		private const string NoStringText = "N/A";

// 		private readonly Dictionary<string, double> _refreshInfoDict = new();

// 		public override void OnInspectorGUI()
// 		{
// 			// Draw default inspector components
// 			base.OnInspectorGUI();

// 			System.Type objectType = target.GetType();
// 			PropertyInfo[] properties = objectType.GetProperties(_BindingFlags);

// 			bool shouldRepaint = false;

// 			foreach (PropertyInfo property in properties)
// 			{
// 				if (property.GetCustomAttribute<InspectorCalculatedPropertyAttribute>() is InspectorCalculatedPropertyAttribute attr
// 					&& (!attr.PlayModeOnly || (EditorApplication.isPlaying && !EditorApplication.isPaused)))
// 				{
// 					if (!_refreshInfoDict.ContainsKey(property.Name) || EditorApplication.timeSinceStartup - _refreshInfoDict[property.Name] > attr.RefreshRate)
// 					{
// 						_refreshInfoDict[property.Name] = EditorApplication.timeSinceStartup;
// 						shouldRepaint = true;

// 						EditorGUI.BeginDisabledGroup(true); // Ensure the field appears read-only
// 						EditorGUILayout.LabelField(property.Name, property.GetValue(target)?.ToString() ?? NoStringText);
// 						EditorGUI.EndDisabledGroup();
// 					}
// 				}
// 			}

// 			// If any property is ready for an update, repaint the editor
// 			if (shouldRepaint)
// 				Repaint();

// 			UnityEngine.Debug.Log($"Repainted: {shouldRepaint}");
// 		}
// 	}
// }
