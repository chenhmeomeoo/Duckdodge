using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KTool.Editor
{
    [CustomEditor(typeof(BecameVisible))]
    public class BecameVisibleEditor : UnityEditor.Editor
    {
        #region Properties
        private SerializedProperty isEnableRender = null;
        private SerializedProperty isCheckVisible = null;
        private SerializedProperty arena = null;
        private BecameVisible becameVisible = null;
        #endregion Properties
        private void OnEnable()
        {
            becameVisible = serializedObject.targetObject as BecameVisible;
            isEnableRender = serializedObject.FindProperty("isEnableRender");
            isCheckVisible = serializedObject.FindProperty("isCheckVisible");
            arena = serializedObject.FindProperty("arena");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //
            EditorGUILayout.PropertyField(isEnableRender, new GUIContent("Enable Render"));
            EditorGUILayout.PropertyField(isCheckVisible, new GUIContent("Check Visible"));
            EditorGUILayout.PropertyField(arena, new GUIContent("Arena"));
            if(!Application.isPlaying)
                becameVisible.GetComponent<SpriteRenderer>().enabled = isEnableRender.boolValue;
            //
            serializedObject.ApplyModifiedProperties();
        }
    }
}
