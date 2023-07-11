using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KTool.Hack
{
    public class TapButton : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private Image imgTap;
        [SerializeField]
        private TextMeshProUGUI txtTap;
        [SerializeField]
        private Color colorNormal,
            colorActive;

        private HackManager hackManager;
        private HackContent hackContent;

        public HackContent HackContent => hackContent;
        #endregion Properties

        #region UnityEvent
        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }
        #endregion UnityEvent

        #region Method
        public void Init(HackManager hackManager, HackContent hackContent)
        {
            this.hackManager = hackManager;
            this.hackContent = hackContent;
            //
            txtTap.text = hackContent.TapName;
            Update_TapSatate();
        }

        public void Update_TapSatate()
        {
            if (hackContent.IsShow)
                imgTap.color = colorActive;
            else
                imgTap.color = colorNormal;
        }

        public void OnClick()
        {
            if (hackContent.IsShow)
                return;
            hackManager.OnClick_Tap(hackContent);
        }
        #endregion Method
    }
}
