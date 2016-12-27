using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WeChatHelper
{
   public static  class UploadCreativeManagement
    {
        /// <summary>
        /// 素材上传
        /// </summary>
        /// <param name="url">微信服务器上传地址</param>
        /// <param name="fileDictionary">待上传的文件（key为上传文件的name，value为本地文件名）例如：key:media,value:"D:/**.jpg"</param>
        /// <returns></returns>
        public static string UploadCreative(string url, Dictionary<string, string> fileDictionary)
        {
            Stream postStream = new MemoryStream();
           // HTTP请求标头值
            string refererUrl=string.Empty;
            //根据传入的url创建请求实例，并设置HTTP请求头部信息
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            var formUploadFile = fileDictionary != null && fileDictionary.Count > 0;
            if (formUploadFile)
            {
                var boundary = "----" + DateTime.Now.Ticks.ToString("*");
                var formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
                foreach (var file in fileDictionary)
                {
                    try
                    {
                        //写入文件流
                        var fileName = file.Value;
                        using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            var formdata = string.Format(formdataTemplate, file.Key,Path.GetFileName(fileName));
                            var formdataBytes = Encoding.ASCII.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);
                            var buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
                var footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);
                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            request.ContentLength = postStream.Length;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Referer = refererUrl;
            }
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1;WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57Safari/537.36";
            {
                postStream.Position = 0;
                var requestStream = request.GetRequestStream();
                var buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                postStream.Close();

            }
            var response = (HttpWebResponse)request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream == null) return null;
                using (var myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    var retString = myStreamReader.ReadToEnd();
                    return retString;
                }

            }

        }

    }
}
