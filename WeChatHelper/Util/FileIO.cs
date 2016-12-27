using System;
using System.IO;
using System.Text;

namespace WeChatHelper.Util
{
    /// <summary>
    /// 文件操作辅助类
    /// </summary>
    public class FileIo
    {        
        private FileStream _fsw;
        private StreamWriter _sw;
        private readonly string _charset = "UTF-8";

        public FileIo()
        {
        }

        public FileIo(string charset)
        {
            _charset = charset;
        }


        /// <summary>
        /// 指定路径创建文件夹
        /// </summary>
        /// <param name="filePath"></param>
        private static void CreateDir(string filePath)
        {
            var dirPath = Path.GetDirectoryName(filePath);
            if(dirPath==null)
                throw new ArgumentNullException(nameof(dirPath));
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void OpenWriteFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    CreateDir(filePath);
                    File.Create(filePath).Close();
                    _fsw = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    _sw = new StreamWriter(_fsw, Encoding.GetEncoding(_charset));
                }
                else
                {
                    _fsw = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    _sw = new StreamWriter(_fsw, Encoding.GetEncoding(_charset));
                }
            }
            catch(Exception er)
            {
                throw new Exception(er.Message);
            }
        }

        public void CloseWriteFile()
        {
            _fsw?.Close();
        }

        public void WriteLine(string s)
        {
            if (_sw == null) return;
            _sw.WriteLine(s);
            _sw.Flush();
        }

        private FileStream _fsr;
        private StreamReader _sr;

        public void OpenReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateDir(filePath);
                File.Create(filePath).Close();
            }
            _fsr = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read,
            FileShare.ReadWrite);
            _sr = new StreamReader(_fsr, Encoding.GetEncoding(_charset));
        }

        public void CloseReadFile()
        {
            _fsr?.Close();
        }

        public string ReadLine()
        {
            return _sr.EndOfStream ? null : _sr.ReadLine();
        }

        public string ReadToEnd()
        {
            return _sr.EndOfStream ? null : _sr.ReadToEnd();
        }

        public bool IsEof()
        {
            return _sr.EndOfStream;
        }
    }
}
