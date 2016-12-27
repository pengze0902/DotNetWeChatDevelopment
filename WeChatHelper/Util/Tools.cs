using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace WeChatHelper.Util
{
    /// <summary>
    /// 辅助工具类
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// 获取Json string某节点的值。
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJosnValue(string jsonStr, string key)
        {
            var result = string.Empty;
            if (string.IsNullOrWhiteSpace(jsonStr)) return result;
            key = "\"" + key.Trim('"') + "\"";
            var index = jsonStr.IndexOf(key, StringComparison.Ordinal) + key.Length + 1;
            if (index <= key.Length + 1) return result;
            var end = jsonStr.IndexOf(',', index);
            if (end == -1)
            {
                end = jsonStr.IndexOf('}', index);
            }
            result = jsonStr.Substring(index, end - index);
            result = result.Trim('"', ' ', '\'');
            return result;
        }


        /// <summary>
        /// datetime转换成unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static  long  ConvertDateTimeInt(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long )(time - startTime).TotalSeconds;
        }

       /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
       /// </summary>
       /// <param name="d"></param>
       /// <returns></returns>
        public static DateTime ConvertIntDateTime(double d)
        {
           var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var time = startTime.AddSeconds(d);
            return time;
        }

        /// <summary>
        /// Json序列化对象
        /// </summary>
        /// <typeparam name="TObjType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static  string ToJsonString<TObjType>(TObjType obj) where TObjType : class
        {
            var jsonSerializer = new JavaScriptSerializer();
            var  s = jsonSerializer.Serialize(obj);
            return s;
        }


        /// <summary>
        /// json字符串的反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static List<T> JsonStringToList<T>(this string jsonStr)
        {
            var serializer = new JavaScriptSerializer();
            var objs = serializer.Deserialize<List<T>>(jsonStr);
            return objs;
        }

        public static T Deserialize<T>(string json)
        {
            var obj = Activator.CreateInstance<T>();
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
