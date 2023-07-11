using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace KTool.KAttribute.Editor
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneAttributeDrawer : PropertyDrawer
    {
        #region Properties
        private string[] scenes;

        #endregion Properties

        #region Constructor
        public SceneAttributeDrawer() : base()
        {
            scenes = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
            {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
            }
        }
        #endregion Constructor

        #region UnityEvent
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.FieldType != typeof(string))
            {
                EditorGUI.LabelField(position, label, new GUIContent("type cua property phai la string"));
                return;
            }
            //
            int index = IndexOf(property.stringValue);
            if(index < 0)
            {
                index = 0;
                property.stringValue = scenes[index];
            }
            //
            int newIndex = EditorGUI.Popup(position, label.text, index, scenes);
            if(newIndex!= index)
            {
                index = newIndex;
                property.stringValue = scenes[index];
            }
        }
        #endregion UnityEvent

        #region Method
        private int IndexOf(string sceneName)
        {
            for (int i = 0; i < scenes.Length; i++)
                if (scenes[i] == sceneName)
                    return i;
            return -1;
        }
        #endregion Method
    }
}
