using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace KTool.Data
{
    public class KList : KObject
    {
        #region Properties
        private const string FORMAT_KEY_ARRAY_COUNT = "{0}[COUNT]",
            FORMAT_KEY_ARRAY_INDEX = "{0}[{1}]";

        private string keyCount;
        private int countentCount;

        public int Count
        {
            set
            {
                countentCount = Mathf.Max(countentCount, value);
                PlayerPrefs.SetInt(keyCount, countentCount);
            }
            get
            {
                return countentCount;
            }
        }
        #endregion Properties

        #region Constructor
        public KList(string keyRoot) : base(keyRoot)
        {
            keyCount = string.Format(FORMAT_KEY_ARRAY_COUNT, keyRoot);
            countentCount = PlayerPrefs.GetInt(keyCount, 0);
        }
        #endregion Constructor

        #region Method
        private string GetKeyContent(int index)
        {
            return string.Format(FORMAT_KEY_ARRAY_INDEX, KeyRoot, index);
        }
        #endregion Method

        #region PlayerPrefs Get
        public int GetInt(int index, int defaultValue)
        {
            if (index < 0 || index >= Count)
                return defaultValue;
            return PlayerPrefs.GetInt(GetKeyContent(index), defaultValue);
        }
        public float GetFloat(int index, float defaultValue)
        {
            if (index < 0 || index >= Count)
                return defaultValue;
            return PlayerPrefs.GetFloat(GetKeyContent(index), defaultValue);
        }
        public string Get(int index, string defaultValue)
        {
            if (index < 0 || index >= Count)
                return defaultValue;
            return PlayerPrefs.GetString(GetKeyContent(index), defaultValue);
        }
        public bool Get(int index, bool defaultValue)
        {
            if (index < 0 || index >= Count)
                return defaultValue;
            return PlayerPrefs.GetInt(GetKeyContent(index), defaultValue ? 1 : 0) == 1;
        }
        public DateTime Get(int index, DateTime defaultValue)
        {
            if (index < 0 || index >= Count)
                return defaultValue;
            return KDictionary.ConvertToDate(PlayerPrefs.GetString(GetKeyContent(index), KDictionary.ConvertToString(defaultValue)));
        }
        public Dictionary<string, object> Get(int index, Dictionary<string, object> defaultValue)
        {
            if (index < 0 || index >= Count)
                return defaultValue;
            return KDictionary.ConvertToDictionary(PlayerPrefs.GetString(GetKeyContent(index), KDictionary.ConvertToString(defaultValue)));
        }
        public List<object> Get(int index, List<object> defaultValue)
        {
            if (index < 0 || index >= Count)
                return defaultValue;
            return KDictionary.ConvertToList(PlayerPrefs.GetString(GetKeyContent(index), KDictionary.ConvertToString(defaultValue)));
        }
        public KDictionary GetKDictionary(int index)
        {
            if (index < 0 || index >= Count)
                return null;
            string childKeyBase = GetKeyContent(index);
            KDictionary newDataObject = new KDictionary(childKeyBase);
            childs.Add(newDataObject);
            return newDataObject;
        }
        public KList GetKList(int index)
        {
            if (index < 0 || index >= Count)
                return null;
            string childKeyBase = GetKeyContent(index);
            KList newDataList = new KList(childKeyBase);
            childs.Add(newDataList);
            return newDataList;
        }
        #endregion PlayerPrefs Get

        #region PlayerPrefs Set
        public void SetInt(int index, int value)
        {
            if (index < 0 || index >= Count)
                return;
            PlayerPrefs.SetInt(GetKeyContent(index), value);
        }
        public void SetFloat(int index, float value)
        {
            if (index < 0 || index >= Count)
                return;
            PlayerPrefs.SetFloat(GetKeyContent(index), value);
        }
        public void Set(int index, string value)
        {
            if (index < 0 || index >= Count)
                return;
            PlayerPrefs.SetString(GetKeyContent(index), value);
        }
        public void Set(int index, bool value)
        {
            if (index < 0 || index >= Count)
                return;
            PlayerPrefs.SetInt(GetKeyContent(index), value ? 1 : 0);
        }
        public void Set(int index, DateTime value)
        {
            if (index < 0 || index >= Count)
                return;
            PlayerPrefs.SetString(GetKeyContent(index), ConvertToString(value));
        }
        public void Set(int index, Dictionary<string, object> value)
        {
            if (index < 0 || index >= Count)
                return;
            PlayerPrefs.SetString(GetKeyContent(index), ConvertToString(value));
        }
        public void Set(int index, List<object> value)
        {
            if (index < 0 || index >= Count)
                return;
            PlayerPrefs.SetString(GetKeyContent(index), ConvertToString(value));
        }
        #endregion PlayerPrefs Set

        #region PlayerPrefs Add
        public void AddInt(int value)
        {
            int index = Count;
            Count += 1;
            PlayerPrefs.SetInt(GetKeyContent(index), value);
        }
        public void AddFloat(float value)
        {
            int index = Count;
            Count += 1;
            PlayerPrefs.SetFloat(GetKeyContent(index), value);
        }
        public void Add(string value)
        {
            int index = Count;
            Count += 1;
            PlayerPrefs.SetString(GetKeyContent(index), value);
        }
        public void Add(bool value)
        {
            int index = Count;
            Count += 1;
            PlayerPrefs.SetInt(GetKeyContent(index), value ? 1 : 0);
        }
        public void Add(DateTime value)
        {
            int index = Count;
            Count += 1;
            PlayerPrefs.SetString(GetKeyContent(index), ConvertToString(value));
        }
        public void Add(Dictionary<string, object> value)
        {
            int index = Count;
            Count += 1;
            PlayerPrefs.SetString(GetKeyContent(index), ConvertToString(value));
        }
        public void Add(List<object> value)
        {
            int index = Count;
            Count += 1;
            PlayerPrefs.SetString(GetKeyContent(index), ConvertToString(value));
        }
        #endregion PlayerPrefs Add
    }
}
