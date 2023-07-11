using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace KTool.Unit.Editor
{
	public class ScriptTemplateKeyWordReplacer : AssetModificationProcessor
	{
		//If there would be more than one keyword to replace, add a Dictionary

		public static void OnWillCreateAsset(string path)
		{
			path = path.Replace(".meta", "");
			int index = path.LastIndexOf(".");
			if (index < 0) 
				return;
			string file = path.Substring(index);
			if (file != ".cs" && file != ".js" && file != ".boo") 
				return;
			index = Application.dataPath.LastIndexOf("Assets");
			path = Application.dataPath.Substring(0, index) + path;
			file = System.IO.File.ReadAllText(path);

			string txtAssets = "Assets/";
			string lastPart = path.Substring(path.IndexOf(txtAssets) + txtAssets.Length);
			string _namespace = lastPart.Substring(0, lastPart.LastIndexOf('/'));
			_namespace = _namespace.Replace('/', '.');
			file = file.Replace("#NAMESPACE#", _namespace);

			System.IO.File.WriteAllText(path, file);
			AssetDatabase.Refresh();
		}
	}
}
