#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JohnCon.Utilities.Editor
{
	/// <summary>
	/// Marks a property as non-editable in the Unity Inspector to enforce its read-only status while
	/// continuing to display it for informational purposes.
	/// </summary>
	public class ReadOnlyAttribute : PropertyAttribute { }

	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public class ReadOnlyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			GUI.enabled = false; // Disable editing
			EditorGUI.PropertyField(position, property, label, true);
			GUI.enabled = true; // Re-enable editing for any following fields
		}
	}
}
#endif
