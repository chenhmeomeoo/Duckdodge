using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace KTool.Localized.Editor
{
	[CustomEditor(typeof(LanguageControl))]
	public class LanguageControlEditor : UnityEditor.Editor
	{
		#region Properties
		private SerializedProperty propertyTexts;
        #endregion Properties

        #region Unity Event
        private void OnEnable()
        {
            propertyTexts = serializedObject.FindProperty("texts");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //
            serializedObject.Update();
            //
            if (GUILayout.Button("Reload Texts"))
                ReloadTexts();
            //
            serializedObject.ApplyModifiedProperties();
        }
        #endregion Unity Event

        #region Method
        private void ReloadTexts()
        {
            LanguageText[] resourceLts = Resources.FindObjectsOfTypeAll<LanguageText>();
            List<LanguageText> sceneLts = new List<LanguageText>();
            for (int i = 0; i < resourceLts.Length; i++)
                if (!EditorUtility.IsPersistent(resourceLts[i].transform.root.gameObject))
                    sceneLts.Add(resourceLts[i]);
            //
            propertyTexts.arraySize = sceneLts.Count;
            if (propertyTexts.arraySize <= 0)
                return;
            int index = 0;
            foreach(SerializedProperty property in propertyTexts)
            {
                property.objectReferenceValue = sceneLts[index];
                index++;
            }
        }
        #endregion Method
    }
}
