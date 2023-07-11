using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniCSV;

namespace KTool.Localized
{
	public class LanguageDataRow
	{
		#region Properties
		private readonly string english;
		private readonly string vietnamese;
		private readonly string portuguese;
		private readonly string spanish;
		private readonly string french;
		private readonly string german;
		private readonly string italian;
		private readonly string korean;
		private readonly string russian;
		private readonly string arabic;
		private readonly string indonesian;
		private readonly string chineseTraditional;
		private readonly string turkish;
		private readonly string japanese;
		#endregion Properties

		#region Constructor
		[CsvConstructor]
		public LanguageDataRow(string english,
			 string vietnamese,
			 string portuguese,
			 string spanish,
			 string french,
			 string german,
			 string italian,
			 string korean,
			 string russian,
			 string arabic,
			 string indonesian,
			 string chineseTraditional,
			 string turkish,
			 string japanese)
        {
			this.english = english;
			this.vietnamese = vietnamese;
			this.portuguese = portuguese;
			this.spanish = spanish;
			this.french = french;
			this.german = german;
			this.italian = italian;
			this.korean = korean;
			this.russian = russian;
			this.arabic = arabic;
			this.indonesian = indonesian;
			this.chineseTraditional = chineseTraditional;
			this.turkish = turkish;
			this.japanese = japanese;
		}
		#endregion Constructor

		#region Method
		public string GetValue(SystemLanguage language)
        {
			switch(language)
            {
				case SystemLanguage.English:
					return english;
				case SystemLanguage.Vietnamese:
					return vietnamese;
				case SystemLanguage.Portuguese:
					return portuguese;
				case SystemLanguage.Spanish:
					return spanish;
				case SystemLanguage.French:
					return french;
				case SystemLanguage.German:
					return german;
				case SystemLanguage.Italian:
					return italian;
				case SystemLanguage.Korean:
					return korean;
				case SystemLanguage.Russian:
					return russian;
				case SystemLanguage.Arabic:
					return arabic;
				case SystemLanguage.Indonesian:
					return indonesian;
				case SystemLanguage.ChineseTraditional:
					return chineseTraditional;
				case SystemLanguage.Turkish:
					return turkish;
				case SystemLanguage.Japanese:
					return japanese;
				default:
					return string.Empty;
			}
        }
		#endregion Method
	}
}
