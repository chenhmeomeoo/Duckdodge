using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Hack
{
    public class HackCode : MonoBehaviour
    {
        #region Properties
        private const string KEY_DATA = HackManager.KEY_DATA + ".HackCode",
            KEY_ACTIVE = KEY_DATA + ".Active";

        [SerializeField]
        private HackButton btnA,
            btnB;
        [SerializeField]
        private bool isEnableHack;
        [SerializeField]
        private int numberA,
            numberB;
        [SerializeField]
        private float timeHack;
        [SerializeField]
        private bool isShowButton;

        private HackManager hackManager;
        private bool isActive;
        private bool isChange;
        private float timeCountdown;

        public bool IsActive
        {
            private set
            {
                isActive = value;
                PlayerPrefs.SetInt(KEY_ACTIVE, isActive ? 1 : 0);
                isChange = true;
            }
            get
            {
                return isActive;
            }
        }
        public bool IsChange => isChange;
        #endregion Properties

        #region UnityEvent
        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            Update_Countdown();
        }
        #endregion UnityEvent

        #region Method
        public void Init(HackManager hackManager)
        {
            this.hackManager = hackManager;
            //
            isActive = PlayerPrefs.GetInt(KEY_ACTIVE, 0) == 1;
            isChange = false;
            timeCountdown = 0;
            btnA.SetShow(isShowButton);
            btnB.SetShow(isShowButton);
        }
        public void Complete_Save()
        {
            isChange = false;
        }
        public void OnClick_ButtonA()
        {
            if (!isEnableHack)
                return;
            if (IsActive)
            {
                hackManager.OnOpen_Hack();
                return;
            }
            //
            if (btnA.Number < numberA)
            {
                if (btnA.Number == 0)
                    timeCountdown = timeHack;
                //
                btnA.Number++;
                if (btnA.Number < numberA)
                    btnA.SetTextColor(Color.red);
                else
                    btnA.SetTextColor(Color.green);
                return;
            }
            if (btnA.Number == numberA)
            {
                if (btnB.Number != numberB)
                {
                    ToDefault();
                    return;
                }
                IsActive = true;
                hackManager.OnOpen_Hack();
                //ToDefault();
            }
        }
        public void OnClick_ButtonB()
        {
            if (!isEnableHack)
                return;
            if (IsActive)
            {
                hackManager.OnOpen_Hack();
                return;
            }
            //
            if (btnA.Number != numberA)
                return;
            if (btnB.Number < numberB)
            {
                btnB.Number++;
                if (btnB.Number < numberB)
                    btnB.SetTextColor(Color.red);
                else
                    btnB.SetTextColor(Color.green);
                return;
            }
            ToDefault();
        }
        private void Update_Countdown()
        {
            if (timeCountdown <= 0)
                return;
            timeCountdown -= Time.deltaTime;
            if (timeCountdown <= 0)
                ToDefault();
        }
        private void ToDefault()
        {
            btnA.Number = 0;
            btnA.SetTextColor(Color.black);
            btnB.Number = 0;
            btnB.SetTextColor(Color.black);
            timeCountdown = 0;
        }
        #endregion Method
    }
}
