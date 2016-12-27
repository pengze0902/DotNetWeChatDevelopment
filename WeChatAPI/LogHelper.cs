using System;



namespace WeChatAPI
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void WriteLog(Type t, Exception ex)
        {
            var log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void WriteLog(Type t, string msg)
        {
            var log = log4net.LogManager.GetLogger(t);
            log.Error(msg);

        }
    }
}

