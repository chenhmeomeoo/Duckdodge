using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using MiniCSV;

namespace KTool.Localized
{
    public class LocalizedManager : MonoBehaviour
    {
        #region Properties
        private const string KEY_LOCALIZED = "LocalizedManager",
            KEY_LANGUAGE = KEY_LOCALIZED + ".Language";
        private const string ERROR_ASSET_FORMAT = "Thiết lập asset format không đúng";

        private static LocalizedManager instance;
        public static LocalizedManager Instance => instance;

        [SerializeField]
        private TextAsset languageAsset;
        [SerializeField]
        private AssetFormat assetFormat;
        [SerializeField]
        private SystemLanguage defaultLanguage;
        [SerializeField]
        private LocalizedData[] localizeds;

        List<LanguageDataRow> languageRows;
        private bool isInit = false;
        private SystemLanguage currentLanguage;
        private event Action onInit,
            onChangeLanguage;

        public bool IsInit => isInit;
        public SystemLanguage DefaultLanguage => defaultLanguage;
        public SystemLanguage CurrentLanguage => currentLanguage;
        #endregion Properties

        #region Unity Event
        private void Awake()
        {
            //
            if (instance == null || instance.GetInstanceID() == GetInstanceID())
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
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDestroy()
        {
            if (instance != null && instance.GetInstanceID() == GetInstanceID())
                instance = null;
        }
        #endregion Unity Event

        #region Method
        private void Init()
        {
            if (isInit)
                return;
            isInit = true;
            //
            SystemLanguage locaLanguage = Application.systemLanguage;
            string strlanguage = PlayerPrefs.GetString(KEY_LANGUAGE, locaLanguage.ToString());
            if (!Enum.TryParse<SystemLanguage>(strlanguage, out currentLanguage))
                currentLanguage = defaultLanguage;
            //
            languageRows = new List<LanguageDataRow>();
            TextReader reader = new StringReader(languageAsset.text);
            CsvDeserializer<LanguageDataRow> deserializer = null;
            try
            {
                CsvParser csvRow = new CsvParser(reader, GetQuote(assetFormat));
                deserializer = new CsvDeserializer<LanguageDataRow>(csvRow);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                Debug.LogError(ERROR_ASSET_FORMAT);
            }
            while (deserializer != null && deserializer.TryProduce(out LanguageDataRow output))
                languageRows.Add(output);
            //
            onInit?.Invoke();
            onChangeLanguage?.Invoke();
        }
        public void SetCurrentLanguage(SystemLanguage language)
        {
            if (language == currentLanguage)
                return;
            currentLanguage = language;
            PlayerPrefs.SetString(KEY_LANGUAGE, language.ToString());
            PlayerPrefs.Save();
            //
            if (IsInit)
                onChangeLanguage?.Invoke();
        }
        public void SetCurrentLanguage(LocalizedData data)
        {
            if (data == null || data.Language == currentLanguage)
                return;
            currentLanguage = data.Language;
            PlayerPrefs.SetString(KEY_LANGUAGE, data.Language.ToString());
            PlayerPrefs.Save();
            //
            if (IsInit)
                onChangeLanguage?.Invoke();
        }
        public void Event_Register(Action onInit, Action onChangeLanguage)
        {
            this.onInit += onInit;
            this.onChangeLanguage += onChangeLanguage;
            if (IsInit)
            {
                onInit?.Invoke();
                onChangeLanguage?.Invoke();
            }
        }
        public void Event_Unregister(Action onInit, Action onChangeLanguage)
        {
            this.onInit -= onInit;
            this.onChangeLanguage -= onChangeLanguage;
        }
        private char GetQuote(AssetFormat assetFormat)
        {
            switch (assetFormat)
            {
                case AssetFormat.MicrosoftExcel:
                    return ';';
                case AssetFormat.GoogleSheets:
                    return ',';
                default:
                    return ' ';
            }
        }
        #region LocalizedData
        public static int Localized_Count()
        {
            if (Instance == null)
                return 0;
            return Instance.LocalizedData_Count();
        }
        public static LocalizedData Localized_Get(int index)
        {
            if (Instance == null)
                return null;
            return Instance.LocalizedData_Get(index);
        }
        public static LocalizedData Localized_GetCurrent()
        {
            if (Instance == null)
                return null;
            return Instance.LocalizedData_GetCurrent();
        }
        public static LocalizedData Localized_GetDefault()
        {
            if (Instance == null)
                return null;
            return Instance.LocalizedData_GetDefault();
        }
        public static Font Localized_GetFont(Font defaultFont)
        {
            LocalizedData localized = Localized_GetCurrent();
            if (localized != null && localized.Font!= null)
                return localized.Font;
            localized = Localized_GetDefault();
            if (localized != null && localized.Font != null)
                return localized.Font;
            return defaultFont;
        }
        private int LocalizedData_Count()
        {
            return localizeds.Length;
        }
        private LocalizedData LocalizedData_Get(int index)
        {
            if (index < 0 || index >= localizeds.Length)
                return null;
            return localizeds[index];
        }
        private LocalizedData LocalizedData_GetCurrent()
        {
            for (int i = 0; i < localizeds.Length; i++)
                if (localizeds[i].Language == currentLanguage)
                    return localizeds[i];
            //
            return null;
        }
        private LocalizedData LocalizedData_GetDefault()
        {
            for (int i = 0; i < localizeds.Length; i++)
                if (localizeds[i].Language == defaultLanguage)
                    return localizeds[i];
            //
            return null;
        }
        #endregion LocalizedData

        #region Language
        public static string Language(string value)
        {
            if (Instance == null)
                return value;
            return Instance.Language_Get(value);
        }
        public static LanguageDataRow LanguageRow(string value)
        {
            if (Instance == null)
                return null;
            return Instance.Language_GetRow(value);
        }
        private LanguageDataRow Language_GetRow(string value)
        {
            if (!IsInit)
                return null;
            for (int i = 0; i < languageRows.Count; i++)
                if (languageRows[i].GetValue(defaultLanguage).ToLower() == value.ToLower())
                    return languageRows[i];
            return null;
        }
        private string Language_Get(string value)
        {
            if (!IsInit)
                return value;
            for (int i = 0; i < languageRows.Count; i++)
                if (languageRows[i].GetValue(defaultLanguage).ToLower() == value.ToLower())
                {
                    string result = languageRows[i].GetValue(currentLanguage);
                    if (string.IsNullOrEmpty(result))
                        result = languageRows[i].GetValue(defaultLanguage);
                    if (string.IsNullOrEmpty(result))
                        return value;
                    return result;
                }
            return value;
        }
        #endregion Language
        #endregion Method
    }
}
