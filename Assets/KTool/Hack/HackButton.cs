using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KTool.Hack
{
    public class HackButton : MonoBehaviour
    {
        #region Properties
        private const string FORMAT_TEXT = "{0} - {1}";

        [SerializeField]
        private Image imgButton;
        [SerializeField]
        private TextMeshProUGUI txtButton;
        [SerializeField]
        private string btnName;

        private int number;

        public bool IsShow
        {
            get
            {
                return imgButton.color.a > 0;
            }
        }
        public int Number
        {
            set
            {
                number = value;
                txtButton.text = string.Format(FORMAT_TEXT, btnName, number);
            }
            get
            {
                return number;
            }
        }
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
        public void SetShow(bool isShow)
        {
            float colorAlpha = isShow ? 1 : 0;
            imgButton.color = new Color(imgButton.color.r, imgButton.color.g, imgButton.color.b, colorAlpha);
            txtButton.color = new Color(txtButton.color.r, txtButton.color.g, txtButton.color.b, colorAlpha);
        }
        public void SetTextColor(Color color)
        {
            txtButton.color = new Color(color.r, color.g, color.b, txtButton.color.a);
        }
        #endregion Method
    }
}
