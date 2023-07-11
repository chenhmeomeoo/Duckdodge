using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KTool.File
{
    public static class Unit
    {
        #region Properties
        private const string FOLDER_DATA = "Data",
            FORMAT_FOLDER = "{0}/{1}";
        private const string TXT_REGULAR = "[^0-9]";
        public static string AssetFolder
        {
            get
            {
                return Application.dataPath;
            }
        }
        public static string ProjectFolder
        {
            get
            {
                DirectoryInfo folder = new DirectoryInfo(AssetFolder);
                return folder.Parent.FullName;
            }
        }
        public static string DataFolder
        {
            get
            {
#if UNITY_EDITOR
                return string.Format(FORMAT_FOLDER, ProjectFolder, FOLDER_DATA);
#elif UNITY_ANDROID || UNITY_IOS
                return Application.persistentDataPath;
#endif
            }
        }

        public static string GetFolder(string folder, PathType pathType)
        {
            switch (pathType)
            {
                case PathType.Data:
                    return string.Format(FORMAT_FOLDER, DataFolder, folder);
                case PathType.Asset:
                    return string.Format(FORMAT_FOLDER, AssetFolder, folder);
            }
            return string.Empty;
        }
        public static string GetFolderOfFile(string pathFile)
        {
            DirectoryInfo folder = new DirectoryInfo(pathFile);
            return folder.FullName;
        }
        public static string GetPathOfAsset(UnityEngine.Object assetObject)
        {
#if UNITY_EDITOR
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(assetObject);
            return string.Format(FORMAT_FOLDER, ProjectFolder, assetPath);
#else
            return string.Empty;
#endif
        }
        #endregion Properties

        #region Method

        #endregion Method

        #region Sort
        public static void SortString(string[] values)
        {
            Array.Sort(values, (a, b) => int.Parse(Regex.Replace(a, TXT_REGULAR, string.Empty)) - int.Parse(Regex.Replace(b, TXT_REGULAR, string.Empty)));
        }
        public static void SortFileInfo(FileInfo[] files)
        {
            Array.Sort(files, (a, b) => int.Parse(Regex.Replace(a.Name, TXT_REGULAR, string.Empty)) - int.Parse(Regex.Replace(b.Name, TXT_REGULAR, string.Empty)));
        }
        public static void SortDirectoryInfo(DirectoryInfo[] directorys)
        {
            Array.Sort(directorys, (a, b) => int.Parse(Regex.Replace(a.Name, TXT_REGULAR, string.Empty)) - int.Parse(Regex.Replace(b.Name, TXT_REGULAR, string.Empty)));
        }
        #endregion Sort
    }
}
