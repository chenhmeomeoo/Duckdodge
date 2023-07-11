using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace KTool.File
{
    public static class FolderFinder
    {
        #region Properties

        #endregion Properties

        #region Method
        public static bool Exists(string folder, PathType pathType)
        {
            if (string.IsNullOrEmpty(folder))
                return false;
            string fullFolder = Unit.GetFolder(folder, pathType);
            return Directory.Exists(fullFolder);
        }
        public static void Delete(string folder, PathType pathType)
        {
            if (string.IsNullOrEmpty(folder))
                return;
            string fullFolder = Unit.GetFolder(folder, pathType);
            if (Directory.Exists(fullFolder))
                Directory.Delete(fullFolder);
        }
        public static void CreateFolder(string folder, PathType pathType)
        {
            if (string.IsNullOrEmpty(folder))
                return;
            string fullFolder = Unit.GetFolder(folder, pathType);
            if (!Directory.Exists(fullFolder))
                Directory.CreateDirectory(fullFolder);
        }
        public static List<string> GetAllFolder(string folder, PathType pathType, bool isSort)
        {
            List<string> result = new List<string>();
            //
            string fullFolder = Unit.GetFolder(folder, pathType);
            DirectoryInfo directory = new DirectoryInfo(fullFolder);
            DirectoryInfo[] folders = directory.GetDirectories();
            if (isSort)
                Unit.SortDirectoryInfo(folders);
            for (int i = 0; i < folders.Length; i++)
            {
                if (string.IsNullOrEmpty(folders[i].Name))
                    continue;
                result.Add(folders[i].Name);
            }
            //
            return result;
        }
        #endregion Method
    }
}
