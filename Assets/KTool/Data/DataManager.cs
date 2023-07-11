using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Data
{
    public class DataManager : MonoBehaviour
    {
        #region Properties
        private static DataManager instance;
        public static DataManager Instance => instance;

        [SerializeField]
        private string rootKey;

        private DataBase[] datas;
        private KDictionary dataDic;

        public KDictionary DataDic => dataDic;
        public int Count => datas.Length;
        public DataBase this[int index]
        {
            get
            {
                if (index < 0 || index >= datas.Length)
                    return null;
                return datas[index];
            }
        }
        #endregion Properties

        #region UnityEvent
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                Init();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }
        private void OnDestroy()
        {
            if (instance != null && instance.GetInstanceID() == GetInstanceID())
                instance = null;
        }
        #endregion UnityEvent

        #region Method
        public void Init()
        {
            dataDic = new KDictionary(rootKey);
            datas = GetDataBaseInchild();
            foreach (var data in datas)
                data.Init(this);
        }

        public T GetDataBase<T>() where T : DataBase
        {
            foreach (var data in datas)
            {
                if (data is T)
                    return data as T;
            }
            return null;
        }
        private DataBase[] GetDataBaseInchild()
        {
            List<DataBase> list = new List<DataBase>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform tfChild = transform.GetChild(i);
                DataBase dataBase = tfChild.GetComponent<DataBase>();
                if (dataBase != null)
                    list.Add(dataBase);
            }
            return list.ToArray();
        }
        #endregion Method
    }
}
