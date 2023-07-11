using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KTool.Data.Editor
{
	public class ClearData
	{
		#region Method
		[MenuItem("KTool/Data/ClearData")]
		public static void OnClick_ClearData()
        {
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
        }
		#endregion Method
	}
}
