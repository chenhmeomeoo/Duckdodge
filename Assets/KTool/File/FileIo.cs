using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTool.File
{
    public static class FileIo
    {
        #region Properties
        private const int BUFFER_SIZE = 8192;
        private const string ERROR_FILE_NOT_FOUND = "File not found";
        #endregion Properties

        #region Text IO
        public static Resoult<string> Text_Read(string folder, string file, ExtensionType extension, PathType pathType, object state = null)
        {
            string path = FileFinder.GetPath(folder, file, extension, pathType);
            return Text_Read(path, state);
        }
        public static Resoult<string> Text_Read(string path, object state = null)
        {
            if (!System.IO.File.Exists(path))
                return Resoult<string>.CreateFail(ERROR_FILE_NOT_FOUND, state);
            //
            Resoult<string> resoult;
            Stream stream = null;
            try
            {
                stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(stream);
                string text = sr.ReadToEnd();
                sr.Close();
                //
                resoult = Resoult<string>.CreateSuccess(text, state);
            }
            catch (Exception ex)
            {
                if (stream != null)
                    stream.Close();
                resoult = Resoult<string>.CreateFail(ex.Message, state);
            }
            //
            return resoult;
        }
        public static Resoult Text_Write(string folder, string file, ExtensionType extension, PathType pathType, string text, object state = null)
        {
            string path = FileFinder.GetPath(folder, file, extension, pathType);
            return Text_Write(path, text, state);
        }
        public static Resoult Text_Write(string path, string text, object state = null)
        {
            string error;
            Stream stream = null;
            try
            {
                if (System.IO.File.Exists(path))
                {
                    stream = System.IO.File.Open(path, FileMode.Truncate, FileAccess.Write);
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(path);
                    DirectoryInfo directory = fileInfo.Directory;
                    if (!directory.Exists)
                        directory.Create();
                    stream = System.IO.File.Open(path, FileMode.Create, FileAccess.Write);
                }
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(text);
                sw.Flush();
                sw.Close();
                //
                error = string.Empty;
            }
            catch (Exception ex)
            {
                if (stream != null)
                    stream.Close();
                error = ex.Message;
            }
            //
            if (string.IsNullOrEmpty(error))
                return Resoult.CreateSuccess(state);
            else
                return Resoult.CreateFail(error, state);
        }
        #endregion

        #region Binary IO
        public static Resoult<byte[]> Binary_Read(string folder, string file, ExtensionType extension, PathType pathType, object state = null)
        {
            string path = FileFinder.GetPath(folder, file, extension, pathType);
            return Binary_Read(path, state);
        }
        public static Resoult<byte[]> Binary_Read(string path, object state = null)
        {
            if (!System.IO.File.Exists(path))
                return Resoult<byte[]>.CreateFail(ERROR_FILE_NOT_FOUND, state);
            //
            Resoult<byte[]> resoult;
            Stream stream = null;
            try
            {
                stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(stream);
                byte[] data = new byte[stream.Length];
                int offset = 0;
                while (offset < data.Length)
                {
                    byte[] buffer = br.ReadBytes(BUFFER_SIZE);
                    buffer.CopyTo(data, offset);
                    offset += buffer.Length;
                }
                stream.Close();
                //
                resoult = Resoult<byte[]>.CreateSuccess(data, state);
            }
            catch (Exception ex)
            {
                if (stream != null)
                    stream.Close();
                resoult = Resoult<byte[]>.CreateFail(ex.Message, state);
            }
            //
            return resoult;
        }
        public static Resoult Binary_Write(string folder, string file, ExtensionType extension, PathType pathType, byte[] data, object state = null)
        {
            string path = FileFinder.GetPath(folder, file, extension, pathType);
            return Binary_Write(path, data, state);
        }
        public static Resoult Binary_Write(string path, byte[] data, object state = null)
        {
            string error;
            Stream stream = null;
            try
            {
                if (System.IO.File.Exists(path))
                {
                    stream = System.IO.File.Open(path, FileMode.Truncate, FileAccess.Write);
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(path);
                    DirectoryInfo directory = fileInfo.Directory;
                    if (!directory.Exists)
                        directory.Create();
                    stream = System.IO.File.Open(path, FileMode.Create, FileAccess.Write);
                }
                int offset = 0;
                while (offset < data.Length)
                {
                    int count = Math.Min(BUFFER_SIZE, data.Length - offset);
                    stream.Write(data, offset, count);
                    offset += count;
                }
                stream.Flush();
                stream.Close();
                //
                error = string.Empty;
            }
            catch (Exception ex)
            {
                if (stream != null)
                    stream.Close();
                error = ex.Message;
            }
            //
            if (string.IsNullOrEmpty(error))
                return Resoult.CreateSuccess(state);
            else
                return Resoult.CreateFail(error, state);
        }
        #endregion
    }
}
