using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool
{
	public class Resoult
	{
		#region Properties
		public readonly bool IsSuccess;
		public readonly string ErrorMessage;
		public readonly object State;
		#endregion Properties

		#region Constructor
		protected Resoult(object state)
		{
			IsSuccess = true;
			ErrorMessage = string.Empty;
			State = state;
		}
		protected Resoult(string message, object state)
		{
			IsSuccess = false;
			ErrorMessage = message;
			State = state;
		}

		public static Resoult CreateSuccess(object state)
        {
			return new Resoult(state);
        }
		public static Resoult CreateFail(string message, object state)
		{
			return new Resoult(message, state);
		}
		#endregion Constructor
	}
}
