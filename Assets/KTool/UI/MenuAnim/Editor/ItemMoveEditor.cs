using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace KTool.UI.MenuAnim.Editor
{
    [CustomEditor(typeof(ItemMove))]
    public class ItemMoveEditor : UnityEditor.Editor
    {
        #region Properties
        private ItemMove itemMove;
        private RectTransform rectTransform;

        private SerializedProperty isShow;
        private SerializedProperty hidePos;
        private SerializedProperty hideTime;
        private SerializedProperty hideEase;
        private SerializedProperty showPos;
        private SerializedProperty showTime;
        private SerializedProperty showEase;
        #endregion Properties

        private void OnEnable()
        {
            itemMove = (ItemMove)target;
            rectTransform = itemMove.GetComponent<RectTransform>();
            isShow = serializedObject.FindProperty("isShow");
            hidePos = serializedObject.FindProperty("hidePos");
            hideTime = serializedObject.FindProperty("hideTime");
            hideEase = serializedObject.FindProperty("hideEase");
            showPos = serializedObject.FindProperty("showPos");
            showTime = serializedObject.FindProperty("showTime");
            showEase = serializedObject.FindProperty("showEase");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //
            bool tmpIsShow =  EditorGUILayout.Toggle(new GUIContent("Is Show"), isShow.boolValue);
            if(tmpIsShow != isShow.boolValue)
            {
                isShow.boolValue = tmpIsShow;
                if(isShow.boolValue)
                {
                    itemMove.gameObject.SetActive(true);
                    rectTransform.anchoredPosition = showPos.vector2Value;
                }
                else
                {
                    itemMove.gameObject.SetActive(false);
                    rectTransform.anchoredPosition = hidePos.vector2Value;
                }
            }
            //
            GUILayout.BeginVertical("Hide", "window");
            Vector2 tmpHidePos = EditorGUILayout.Vector2Field(new GUIContent("Hide Pos"), hidePos.vector2Value);
            if (tmpHidePos != hidePos.vector2Value)
            {
                hidePos.vector2Value = tmpHidePos;
                if(!isShow.boolValue)
                    rectTransform.anchoredPosition = hidePos.vector2Value;
            }
            EditorGUILayout.PropertyField(hideTime, new GUIContent("Time"));
            EditorGUILayout.PropertyField(hideEase, new GUIContent("Ease"));
            GUILayout.EndVertical();
            //
            GUILayout.BeginVertical("Show", "window");
            Vector2 tmpShowPos = EditorGUILayout.Vector2Field(new GUIContent("Show Pos"), showPos.vector2Value);
            if (tmpShowPos != showPos.vector2Value)
            {
                showPos.vector2Value = tmpShowPos;
                if(isShow.boolValue)
                    rectTransform.anchoredPosition = showPos.vector2Value;
            }
            EditorGUILayout.PropertyField(showTime, new GUIContent("Time"));
            EditorGUILayout.PropertyField(showEase, new GUIContent("Ease"));
            GUILayout.EndVertical();
            //
            hideTime.floatValue = Mathf.Max(0, hideTime.floatValue);
            showTime.floatValue = Mathf.Max(0, showTime.floatValue);
            EditPos();
            //
            serializedObject.ApplyModifiedProperties();
        }
        private void EditPos()
        {
            if (EditorApplication.isPlaying)
                return;
            if (isShow.boolValue)
            {
                if (showPos.vector2Value != rectTransform.anchoredPosition)
                    showPos.vector2Value = rectTransform.anchoredPosition;
            }
            else
            {
                if (hidePos.vector2Value != rectTransform.anchoredPosition)
                    hidePos.vector2Value = rectTransform.anchoredPosition;
            }
        }
    }
}
