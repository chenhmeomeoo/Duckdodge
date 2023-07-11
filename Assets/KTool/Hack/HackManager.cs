using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KTool.UI.MenuAnim;

namespace KTool.Hack
{
    public class HackManager : MonoBehaviour
    {
        #region Properties
        public const string KEY_DATA = "HackManager";

        [SerializeField]
        private TapButton prefabTapButton;
        [SerializeField]
        private HackCode hackCode;
        [SerializeField]
        private HackContent[] hackContents;

        private MenuControl menuControl;
        private List<TapButton> tapButtons;

        private MenuControl MenuControl
        {
            get
            {
                if (menuControl == null)
                    menuControl = GetComponent<MenuControl>();
                return menuControl;
            }
        }
        public bool IsShow => MenuControl.IsShow;
        public bool IsChange
        {
            get
            {
                if (hackCode.IsChange)
                    return true;
                for (int i = 0; i < hackContents.Length; i++)
                    if (hackContents[i].IsChange)
                        return true;
                return false;
            }
        }
        #endregion Properties

        #region UnityEvent
        // Start is called before the first frame update
        private void Start()
        {
            Init();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            Update_AutoSave();
        }
        #endregion UnityEvent

        #region Method
        public void Init()
        {
            hackCode.Init(this);
            for (int i = 0; i < hackContents.Length; i++)
                hackContents[i].Init(this);
            //
            tapButtons = new List<TapButton>();
            for (int i = 0; i < hackContents.Length; i++)
            {
                TapButton newTapButton = Instantiate(prefabTapButton, prefabTapButton.transform.parent);
                newTapButton.gameObject.SetActive(true);
                newTapButton.Init(this, hackContents[i]);
                tapButtons.Add(newTapButton);
            }
        }
        private void Update_AutoSave()
        {
            if (!IsChange)
                return;
            PlayerPrefs.Save();
            hackCode.Complete_Save();
            for (int i = 0; i < hackContents.Length; i++)
                hackContents[i].Complete_Save();
        }
        public void OnOpen_Hack()
        {
            hackContents[0].Show();
            foreach (TapButton tap in tapButtons)
                tap.Update_TapSatate();
            //
            MenuControl.PlayShow();
        }
        public void OnClick_Close()
        {
            for (int i = 0; i < hackContents.Length; i++)
                hackContents[i].Hide();
            foreach (TapButton tap in tapButtons)
                tap.Update_TapSatate();
            for (int i = 0; i < hackContents.Length; i++)
                hackContents[i].OnClose();
            //
            MenuControl.PlayHide();
        }

        public void OnClick_Tap(HackContent hackContent)
        {
            for (int i = 0; i < hackContents.Length; i++)
            {
                if (hackContents[i].GetInstanceID() == hackContent.GetInstanceID())
                    hackContents[i].Show();
                else
                    hackContents[i].Hide();
            }
            foreach (TapButton tap in tapButtons)
                tap.Update_TapSatate();
        }
        #endregion Method
    }
}
