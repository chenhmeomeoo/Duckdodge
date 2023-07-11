using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Localized
{
    public class LanguageControl : MonoBehaviour
    {
        #region Properties
        private static LanguageControl instance;
        public static LanguageControl Instance => instance;

        [SerializeField]
        private LanguageText[] texts;

        private List<LanguageText> listText;

        #endregion Properties

        #region Unity Event
        private void OnDestroy()
        {
            if (instance != null && instance.GetInstanceID() == GetInstanceID())
            {
                instance = null;
                if (LocalizedManager.Instance != null)
                    LocalizedManager.Instance.Event_Unregister(OnInit, OnChange_Language);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null || instance.GetInstanceID() == GetInstanceID())
            {
                instance = this;
                listText = new List<LanguageText>();
                if(LocalizedManager.Instance != null)
                    LocalizedManager.Instance.Event_Register(OnInit, OnChange_Language);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion Unity Event

        #region Method
        private void OnInit()
        {
            for (int i = 0; i < texts.Length; i++)
                if (texts[i] != null)
                    texts[i].OnInit();
            for (int i = 0; i < listText.Count; i++)
                if (listText[i] != null)
                    listText[i].OnInit();
        }
        private void OnChange_Language()
        {
            for (int i = 0; i < texts.Length; i++)
                if (texts[i] != null)
                    texts[i].OnChange_Language();
            for (int i = 0; i < listText.Count; i++)
                if (listText[i] != null)
                    listText[i].OnChange_Language();
        }
        public int Text_Count()
        {
            return listText.Count;
        }
        public void Text_Add(LanguageText text)
        {
            if (text == null)
                return;
            //
            int id = text.GetInstanceID();
            for (int i = 0; i < listText.Count; i++)
                if (id == listText[i].GetInstanceID())
                    return;
            listText.Add(text);
            //
            if (LocalizedManager.Instance != null &&  LocalizedManager.Instance.IsInit)
            {
                text.OnInit();
                if (LocalizedManager.Instance.CurrentLanguage != LocalizedManager.Instance.DefaultLanguage)
                    text.OnChange_Language();
            }
        }
        public void Text_Remove(LanguageText text)
        {
            if (text == null)
                return;
            //
            int id = text.GetInstanceID(),
                index = 0;
            while (index < listText.Count)
            {
                if (id == listText[index].GetInstanceID())
                {
                    listText.RemoveAt(index);
                    return;
                }
                index++;
            }
        }
        #endregion Method
    }
}
