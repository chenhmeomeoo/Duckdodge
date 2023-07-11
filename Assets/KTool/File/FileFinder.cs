using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.File
{
    public class FileFinder
    {
        #region Properties
        private const char CHAR_SPECIAL = '.';
        private const string FOTMAT_FULL_PATH = "{0}/{1}.{2}",
            FORMAT_SEARCH_PATTERN = "*.{0}";
        #endregion Properties

        #region Method
        public static string GetExtension(ExtensionType extension)
        {
            return extension.ToString().ToLower();
        }
        public static string GetSearchPattern(ExtensionType extension)
        {
            return string.Format(FORMAT_SEARCH_PATTERN, GetExtension(extension));
        }
        public static string GetPath(string folder, string fileName, ExtensionType extension, PathType pathType)
        {
            return string.Format(FOTMAT_FULL_PATH, Unit.GetFolder(folder, pathType), fileName, GetExtension(extension));
        }
        public static string GetPath(string folder, string fileName, string extension, PathType pathType)
        {
            return string.Format(FOTMAT_FULL_PATH, Unit.GetFolder(folder, pathType), fileName, extension.ToLower());
        }
        public static bool Exists(string folder, string file, ExtensionType extension, PathType pathType)
        {
            if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(file))
                return false;
            string path = GetPath(folder, file, extension, pathType);
            return System.IO.File.Exists(path);
        }
        public static bool Exists(string folder, string file, string extension, PathType pathType)
        {
            if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(file))
                return false;
            string path = GetPath(folder, file, extension, pathType);
            return System.IO.File.Exists(path);
        }
        public static void Delete(string folder, string file, ExtensionType extension, PathType pathType)
        {
            if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(file))
                return;
            string path = GetPath(folder, file, extension, pathType);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
        public static void Delete(string folder, string file, string extension, PathType pathType)
        {
            if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(file))
                return;
            string path = GetPath(folder, file, extension, pathType);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
        public static List<string> GetAllFile(string folder, ExtensionType extension, PathType pathType, bool isSort)
        {
            if (string.IsNullOrEmpty(folder))
                return new List<string>();
            string fullFolder = Unit.GetFolder(folder, pathType);
            string searchPattern = GetSearchPattern(extension);
            DirectoryInfo directory = new DirectoryInfo(fullFolder);
            FileInfo[] files = directory.GetFiles(searchPattern);
            if (isSort)
                Unit.SortFileInfo(files);

            //
            List<string> result = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                string name = files[i].Name.Substring(0, files[i].Name.LastIndexOf(CHAR_SPECIAL));
                if (string.IsNullOrEmpty(name))
                    continue;
                result.Add(name);
            }
            return result;
        }
        #endregion Method
    }
}
