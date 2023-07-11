using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using KTool.Unit;

namespace KTool.Data
{
    public class KDictionary : KObject
    {
        #region Properties
        private const string FORMAT_KEY_CONTENT = "{0}.{1}";
        #endregion Properties

        #region Constructor
        public KDictionary(string keyRoot) : base(keyRoot)
        {

        }
        #endregion Constructor

        #region Method
        private string GetKeyContent(string keyContent)
        {
            return string.Format(FORMAT_KEY_CONTENT, KeyRoot, keyContent);
        }
        #endregion Method

        #region PlayerPrefs Get
        public int GetInt(string keyContent, int defaultValue)
        {
            return PlayerPrefs.GetInt(GetKeyContent(keyContent), defaultValue);
        }
        public float GetFloat(string keyContent, float defaultValue)
        {
            return PlayerPrefs.GetFloat(GetKeyContent(keyContent), defaultValue);
        }
        public string Get(string keyContent, string defaultValue)
        {
            return PlayerPrefs.GetString(GetKeyContent(keyContent), defaultValue);
        }
        public bool Get(string keyContent, bool defaultValue)
        {
            return PlayerPrefs.GetInt(GetKeyContent(keyContent), defaultValue ? 1 : 0) == 1;
        }
        public DateTime Get(string keyContent, DateTime defaultValue)
        {
            return ConvertToDate(PlayerPrefs.GetString(GetKeyContent(keyContent), ConvertToString(defaultValue)));
        }
        public Dictionary<string, object> Get(string keyContent, Dictionary<string, object> defaultValue)
        {
            return ConvertToDictionary(PlayerPrefs.GetString(GetKeyContent(keyContent), ConvertToString(defaultValue)));
        }
        public List<object> Get(string keyContent, List<object> defaultValue)
        {
            return ConvertToList(PlayerPrefs.GetString(GetKeyContent(keyContent), ConvertToString(defaultValue)));
        }
        public KDictionary GetKDictionary(string keyContent)
        {
            string childKeyBase = GetKeyContent(keyContent);
            KDictionary newKDictionary = new KDictionary(childKeyBase);
            childs.Add(newKDictionary);
            return newKDictionary;
        }
        public KList GetKList(string keyContent)
        {
            string childKeyBase = GetKeyContent(keyContent);
            KList newDataList = new KList(childKeyBase);
            childs.Add(newDataList);
            return newDataList;
        }
        #endregion PlayerPrefs Get

        #region PlayerPrefs Set
        public void SetInt(string keyContent, int value)
        {
            PlayerPrefs.SetInt(GetKeyContent(keyContent), value);
        }
        public void SetFloat(string keyContent, float value)
        {
            PlayerPrefs.SetFloat(GetKeyContent(keyContent), value);
        }
        public void Set(string keyContent, string value)
        {
            PlayerPrefs.SetString(GetKeyContent(keyContent), value);
        }
        public void Set(string keyContent, bool value)
        {
            PlayerPrefs.SetInt(GetKeyContent(keyContent), value ? 1 : 0);
        }
        public void Set(string keyContent, DateTime value)
        {
            PlayerPrefs.SetString(GetKeyContent(keyContent), ConvertToString(value));
        }
        public void Set(string keyContent, Dictionary<string, object> value)
        {
            PlayerPrefs.SetString(GetKeyContent(keyContent), ConvertToString(value));
        }
        public void Set(string keyContent, List<object> value)
        {
            PlayerPrefs.SetString(GetKeyContent(keyContent), ConvertToString(value));
        }
        #endregion PlayerPrefs Set
    }
}
