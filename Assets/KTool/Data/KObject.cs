using KTool.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace KTool.Data
{
	public abstract class KObject
	{
        #region Properties
        private const string TIME_CULTURE = "en-US";

        private static CultureInfo dateTimeCultureInfo;
        public static CultureInfo DateTimeCultureInfo
        {
            get
            {
                if (dateTimeCultureInfo == null)
                    dateTimeCultureInfo = new CultureInfo(TIME_CULTURE);
                return dateTimeCultureInfo;
            }
        }

        private string keyRoot;
        protected List<KObject> childs;

        protected string KeyRoot => keyRoot;
        #endregion Properties

        #region Construction
        public KObject(string keyRoot)
		{
            this.keyRoot = keyRoot;
            childs = new List<KObject>();
        }
        #endregion Construction

        #region Method

        #endregion Method

        #region Convert
        public static string ConvertToString(DateTime dateTime)
        {
            return dateTime.ToString(DateTimeCultureInfo);
        }
        public static string ConvertToString(Dictionary<string, object> dic)
        {
            return Json.Serialize(dic);
        }
        public static string ConvertToString(List<object> list)
        {
            return Json.Serialize(list);
        }
        public static DateTime ConvertToDate(string time)
        {
            return Convert.ToDateTime(time, DateTimeCultureInfo);
        }
        public static Dictionary<string, object> ConvertToDictionary(string jsonData)
        {
            return Json.Deserialize(jsonData) as Dictionary<string, object>;
        }
        public static List<object> ConvertToList(string jsonData)
        {
            return Json.Deserialize(jsonData) as List<object>;
        }
        #endregion Convert
    }
}
