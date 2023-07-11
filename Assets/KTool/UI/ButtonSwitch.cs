using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace KTool.UI
{
	public class ButtonSwitch : MonoBehaviour
	{
		#region Properties
		[SerializeField]
		private Image imgOn,
			imgOff;
		[SerializeField]
		private UnityEvent onSwitch;

		public bool Switch
        {
			set
            {
				imgOn.gameObject.SetActive(value);
				imgOff.gameObject.SetActive(!value);
			}
            get
            {
				return imgOn.gameObject.activeSelf;
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

		public void OnClick_Switch()
        {
			Switch = !Switch;
			onSwitch?.Invoke();
		}
	}
}
