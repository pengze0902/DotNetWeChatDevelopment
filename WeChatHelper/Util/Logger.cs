using System;

namespace WeChatHelper.Util
{
    /// <summary>
    /// 写文本日志
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// 写文本日志
        /// </summary>
        /// <param name="strContent">日志内容</param>
        /// <param name="logFilePath">日志路径</param>
        public static void WriteTxtLog(string strContent, string logFilePath)
        {
            try
            {
                var fileName = logFilePath;
                const string br = "\r\n";
                var content = strContent;
                var fIo = new FileIo();
                fIo.OpenWriteFile(fileName);
                fIo.WriteLine(content + br + br + "*******************************************************" + br);
                fIo.CloseWriteFile();
            }
            catch(Exception er)
            {
                throw new Exception(er.Message);
            }
        }

        /// <summary>
        /// 写文本日志
        /// </summary>
        /// <param name="strContent"></param>
        public static void WriteTxtLog(string strContent)
        {
            try
            {
                var fileName = AppDomain.CurrentDomain.BaseDirectory;
                if (!fileName.EndsWith("\\"))
                {
                    fileName += "\\";
                }
                fileName += "Log\\";
                fileName += DateTime.Now.ToString("yyyy-MM-dd-HH") + ".txt";
                const string br = "\r\n";
                var content = strContent;
                var fIo = new FileIo();
                fIo.OpenWriteFile(fileName);
                fIo.WriteLine(content + br + br + "*******************************************************" + br);
                fIo.CloseWriteFile();
            }
            catch(Exception er)
            {
                throw new Exception(er.Message);
            }
        }

        /// <summary>
        /// 异常写文本日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteTxtLog(Exception ex)
        {
            try
            {
                var fileName = AppDomain.CurrentDomain.BaseDirectory;
                if (!fileName.EndsWith("\\"))
                {
                    fileName += "\\";
                }
                fileName += "Log\\";
                fileName += DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                const string blank = "    ";
                const string br = "\r\n";
                var content = string.Empty;
                content += "客户端IP：" + ClientIp;
                content += br + "客户端操作系统：" + ClientPlatform;
                content += br + "客户端浏览器：" + ClientBrowser;
                content += br + "服务器计算机名：" + System.Net.Dns.GetHostName();
                content += br + "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                content += br + "引发页面：" + System.Web.HttpContext.Current.Request.Url;
                content += br + "异常对象：" + ex.Source;
                content += br + "异常信息：" + ex.Message;
                content += br + "异常方法：" + ex.TargetSite;
                content += br + "错误详细信息：";
                content += br + blank + ex;
                var fIo = new FileIo();
                fIo.OpenWriteFile(fileName);
                fIo.WriteLine(content + br + br + "*******************************************************" + br);
                fIo.CloseWriteFile();
            }
            catch(Exception er)
            {
                throw new Exception(er.Message);
            }
        }



        /// <summary>
        /// 客户端IP
        /// </summary>
        private static string ClientIp
        {
            get
            {
                var result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(result))
                {
                    result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                return string.IsNullOrEmpty(result) ? "0.0.0.0" : result;
            }
        }



        /// <summary>
        /// 客户端使用平台的名字 
        /// </summary>
        private static string ClientPlatform
        {
            get
            {
                try
                {
                    return System.Web.HttpContext.Current.Request.Browser.Platform;
                }
                catch(Exception er)
                {
                    throw new Exception(er.Message);
                }
            }
        }


        /// <summary>
        /// 客户端浏览器
        /// </summary>
        private static string ClientBrowser
        {
            get
            {
                try
                {
                    var bc = System.Web.HttpContext.Current.Request.Browser;
                    return bc.Browser + " v." + bc.Version;
                }
                catch(Exception er)
                {
                   throw new Exception(er.Message);
                }
            }
        }

    }
}
