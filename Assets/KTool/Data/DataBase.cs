using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Data
{
    public abstract class DataBase : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private string dataName;

        private DataManager dataManager;
        private KDictionary data;

        public DataManager DataManager => dataManager;
        protected KDictionary Data => data;
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
        public void Init(DataManager dataManager)
        {
            this.dataManager = dataManager;
            data = dataManager.DataDic.GetKDictionary(dataName);
            OnInit();
        }

        protected abstract void OnInit();
        #endregion Method
    }
}
