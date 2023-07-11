using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.Hack
{
	public abstract class HackContent : MonoBehaviour
	{
		#region Properties
		public const string KEY_DATA = HackManager.KEY_DATA + ".Content";

		[SerializeField]
		private string tapName;

		private HackManager hackManager;
		private bool isShow;
		private bool isChange;

		public HackManager HackManager => hackManager;
		public bool IsShow => isShow;
		public string TapName => tapName;
		public bool IsChange
		{
			protected set
			{
				isChange = value;
			}
            get
            {
				return isChange;
            }
        }
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
		public void Init(HackManager hackManager)
        {
			this.hackManager = hackManager;
			//
			isChange = false;
			gameObject.SetActive(false);
			OnInit();
		}
		public void Show()
        {
			isShow = true;
			gameObject.SetActive(true);
			OnShow();
		}
		public void Hide()
        {
			isShow = false;
			gameObject.SetActive(false);
			OnHide();
		}
		public virtual void OnClose()
        {

        }
		public void Complete_Save()
        {
			isChange = false;
        }

		protected abstract void OnInit();
		protected abstract void OnShow();
		protected abstract void OnHide();
		#endregion Method
	}
}
