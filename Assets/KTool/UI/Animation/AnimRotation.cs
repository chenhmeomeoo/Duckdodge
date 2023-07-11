using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.UI.Animation
{
	public class AnimRotation : MonoBehaviour
	{
		#region Properties
		[SerializeField]
		private float velocity = 0;
		[SerializeField]
		private bool unScaleTime = false;
		#endregion Properties

		#region UnityEvent
		// Start is called before the first frame update
		void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			Animation();
		}
		#endregion UnityEvent	
		
		private void Animation()
        {
			if (velocity == 0)
				return;
			float deltaTime = unScaleTime ? Time.unscaledDeltaTime : Time.deltaTime;
			transform.Rotate(0, 0, velocity * deltaTime);
		}
	}
}
