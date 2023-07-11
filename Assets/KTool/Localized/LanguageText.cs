using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Localized
{
    public abstract class LanguageText : MonoBehaviour
    {
        #region Properties
        private const string FORMAT_VALUE = "{0}{1}{2}";
        private const char ENTRIES = ' ';

        [SerializeField]
        private string prefix,
            suffix;
        [SerializeField]
        private TextFormat format;

        private LanguageDataRow languageData;
        private string defaultValue;
        private Font defaultFont;
        private bool isInit;

        public string Prefix => prefix;
        public string Suffix => suffix;
        public TextFormat Format => format;
        public abstract string Value
        {
            set;
            get;
        }
        public abstract Font FontText
        {
            set;
            get;
        }
        #endregion Properties

        #region Method
        public void OnInit()
        {
            if (isInit)
            {
                //Debug.LogError( name + "Text is Init");
                return;
            }
            isInit = true;
            //
            defaultValue = Value;
            defaultFont = FontText;
            languageData = LocalizedManager.LanguageRow(defaultValue);
        }
        public void OnChange_Language()
        {
            if (languageData == null)
                return;
            //
            LocalizedData localizedData = LocalizedManager.Localized_GetCurrent();
            if (localizedData.Font == null)
                localizedData = LocalizedManager.Localized_GetDefault();
            if (localizedData.Font == null)
                FontText = defaultFont;
            else 
                FontText = localizedData.Font;
            //
            string tmpValue = languageData.GetValue(LocalizedManager.Instance.CurrentLanguage);
            if (string.IsNullOrEmpty(tmpValue))
                tmpValue = languageData.GetValue(LocalizedManager.Instance.DefaultLanguage);
            if (string.IsNullOrEmpty(tmpValue))
                tmpValue = defaultValue;
            Value = GetValue(tmpValue);
        }
        private string GetValue(string value)
        {
            if (!string.IsNullOrEmpty(Suffix) || !string.IsNullOrEmpty(Prefix))
                value = string.Format(FORMAT_VALUE, Prefix, value, Suffix);
            switch (Format)
            {
                case TextFormat.Lower:
                    return GetValue_Lower(value);
                case TextFormat.Upper:
                    return GetValue_Upper(value);
                case TextFormat.Sentence:
                    return GetValue_Sentence(value);
                case TextFormat.Title:
                    return GetValue_Title(value);
                default:
                    return value;
            }
        }
        public static string GetValue_Lower(string value)
        {
            return value.ToLower();
        }
        public static string GetValue_Upper(string value)
        {
            return value.ToUpper();
        }
        public static string GetValue_Sentence(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (value.Length == 1)
                return value.ToUpper();
            //
            string first = value.Substring(0, 1),
                end = value.Substring(1);
            return first.ToUpper() + end;
        }
        public static string GetValue_Title(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (value.Length == 1)
                return value.ToUpper();
            //
            string result = string.Empty;
            string[] units = value.Split(new char[] { ENTRIES }, System.StringSplitOptions.None);
            for (int i = 0; i < units.Length; i++)
            {
                if (i < units.Length - 1)
                    result += GetValue_Sentence(units[i]) + ENTRIES;
                else
                    result += GetValue_Sentence(units[i]);
            }
            return result;
        }
        #endregion Method
    }
}
