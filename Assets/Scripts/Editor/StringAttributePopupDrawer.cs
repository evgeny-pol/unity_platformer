using System;
using UnityEditor;
using UnityEngine;

public abstract class StringAttributePopupDrawer : PropertyDrawer
{
    protected abstract string[] Values { get; }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] values = Values;
        int index = Mathf.Max(0, Array.IndexOf(values, property.stringValue));
        index = EditorGUI.Popup(position, property.displayName, index, values);
        property.stringValue = values[index];
    }
}
