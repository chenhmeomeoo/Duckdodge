using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KTool.Localized
{
	[RequireComponent(typeof(UnityEngine.UI.Text))]
	public class LanguageTextUI : LanguageText
	{
		#region Properties
		private UnityEngine.UI.Text thisText;

		private UnityEngine.UI.Text ThisText
        {
            get
            {
				if (thisText == null)
					thisText = GetComponent<UnityEngine.UI.Text>();
				return thisText;
			}
        }
		public override string Value
		{
			set
            {
				ThisText.text = value;

			}
			get
            {
				return ThisText.text;

			}
		}

        public override Font FontText
		{
			set
            {
				ThisText.font = value;

			}
			get
            {
				return ThisText.font;

			}
		}
        #endregion Properties

        #region Unity Event
        // Start is called before the first frame update
        void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			
		}
		#endregion Unity Event
		
		#region Method
		
		#endregion Method
	}
}
