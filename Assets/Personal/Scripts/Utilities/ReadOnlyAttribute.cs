using UnityEditor;
using UnityEngine;

namespace JohnCon.Util.Editor
{
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
