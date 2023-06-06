using UnityEngine;
using System;
using System.Linq;
using UnityEditor;

namespace Puertas.Variables
{

    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);
            // Create property fields.
            bool useConstant = property.FindPropertyRelative("UseConstant").boolValue;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var rect = new Rect(position.position, Vector2.one * 20);

            var dropDownButton = EditorGUI.DropdownButton(
                rect,
                new GUIContent(GetTexture()),
                FocusType.Keyboard);

            if (dropDownButton)
            {

                GenericMenu menu = new GenericMenu();

                menu.AddItem(
                    new GUIContent("Constant"),
                    useConstant,
                    () => SetProperty(property, true));

                menu.AddItem(
                    new GUIContent("Variable"),
                    !useConstant,
                    () => SetProperty(property, false));

                menu.ShowAsContext();

            }

            position.position += Vector2.right * 15;
            position.size -= Vector2.right * 15;

            int value = property.FindPropertyRelative("ConstantValue").intValue;

            if (useConstant)
            {
                string newValue = EditorGUI.TextField(position, value.ToString());
                int.TryParse(newValue, out value);
                property.FindPropertyRelative("ConstantValue").intValue = value;
            }
            else
            {
                EditorGUI.ObjectField(position, property.FindPropertyRelative("Variable"), GUIContent.none);
            }

            EditorGUI.EndProperty();

        }

        private void SetProperty(SerializedProperty property, bool value)
        {
            var propRelative = property.FindPropertyRelative("UseConstant");
            propRelative.boolValue = value;
            property.serializedObject.ApplyModifiedProperties();
        }

        private Texture GetTexture()
        {
            var textures = Resources.FindObjectsOfTypeAll(typeof(Texture))
                .Where(t => t.name.ToLower().Contains("arrow"))
                .Cast<Texture>().ToList();

            return textures[0];
        }
    }
}