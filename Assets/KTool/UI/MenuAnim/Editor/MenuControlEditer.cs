using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KTool.UI.MenuAnim.Editor
{
    [CustomEditor(typeof(MenuControl))]
    public class MenuControlEditer : UnityEditor.Editor
    {
        #region Properties
        private SerializedProperty isShow;
        private SerializedProperty unScaleTime;
        private SerializedProperty items;

        private MenuControl menuAnim = null;
        private bool isShowItems = false;
        #endregion Properties
        private void OnEnable()
        {
            isShow = serializedObject.FindProperty("isShow");
            unScaleTime = serializedObject.FindProperty("unScaleTime");
            items = serializedObject.FindProperty("items");
            //
            menuAnim = serializedObject.targetObject as MenuControl;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //
            EditorGUILayout.PropertyField(isShow, new GUIContent("IsShow"));
            EditorGUILayout.PropertyField(unScaleTime, new GUIContent("Unscale Time"));
            ShowEditorItems();
            //
            serializedObject.ApplyModifiedProperties();
        }

        private void ShowEditorItems()
        {
            int count = items.arraySize;
            count = EditorGUILayout.IntField("Item Count", count);
            if (items.arraySize != count)
                items.arraySize = count;
            if (count <= 0)
            {
                if (GUILayout.Button("Reload Items"))
                    LoadItems();
                return;
            }
            //
            if (isShowItems)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Reload Items"))
                    LoadItems();
                if (GUILayout.Button("Hide Items"))
                {
                    isShowItems = false;
                    return;
                }
                GUILayout.EndHorizontal();
                //
                int index = 0;
                foreach(SerializedProperty item in items)
                {
                    SerializedProperty propertyItem = item.FindPropertyRelative("item"),
                        propertyDelayShow = item.FindPropertyRelative("delayShow"),
                        propertyDelayHide = item.FindPropertyRelative("delayHide");
                    //
                    GUILayout.BeginVertical(propertyItem.objectReferenceValue.name, "window");
                    EditorGUILayout.PropertyField(propertyItem, new GUIContent("Item"));
                    EditorGUILayout.PropertyField(propertyDelayShow, new GUIContent("Delay Show"));
                    EditorGUILayout.PropertyField(propertyDelayHide, new GUIContent("Delay Hide"));
                    if (GUILayout.Button("Remove"))
                    {
                        items.DeleteArrayElementAtIndex(index);
                        GUILayout.EndVertical();
                    }
                    else
                    {
                        GUILayout.EndVertical();
                        index++;
                    }
                }
            }
            else
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Reload Items"))
                    LoadItems();
                if (GUILayout.Button("Show Items"))
                {
                    isShowItems = true;
                    return;
                }
                GUILayout.EndHorizontal();
            }
        }

        private void LoadItems()
        {
            if (menuAnim == null)
                return;
            List<MenuAnim.Item> listItem = LoadTransform_Items(menuAnim.transform);
            if (listItem == null || listItem.Count <= 0)
            {
                items.arraySize = 0;
                return;
            }
            items.arraySize = listItem.Count;
            int index = 0;
            foreach (SerializedProperty item in items)
            {
                SerializedProperty propertyItem = item.FindPropertyRelative("item");
                propertyItem.objectReferenceValue = listItem[index];
                index++;
            }
        }
        private List<MenuAnim.Item> LoadTransform_Items(Transform transform)
        {
            if (transform == null || transform.childCount == 0)
                return null;
            List<MenuAnim.Item> listItem = new List<MenuAnim.Item>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform childTF = transform.GetChild(i);
                MenuAnim.Item item = childTF.GetComponent<MenuAnim.Item>();
                if (item != null)
                    listItem.Add(item);
                MenuControl mc = childTF.GetComponent<MenuControl>();
                if (mc == null)
                {
                    List<MenuAnim.Item> childList = LoadTransform_Items(childTF);
                    if (childList != null && childList.Count > 0)
                        listItem.AddRange(childList);
                }
            }
            return listItem;
        }
    }
}
