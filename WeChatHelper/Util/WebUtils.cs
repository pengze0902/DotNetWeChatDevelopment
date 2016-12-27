using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Image = System.Web.UI.WebControls.Image;

namespace WeChatHelper.Util
{
    /// <summary>
    /// 网络工具类。
    /// </summary>
    public sealed class WebUtils
    {
        /// <summary>
        /// 请求与响应的超时时间
        /// </summary>
        public int Timeout { get; set; } = 100000;

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">post数据</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, string postData)
        {
            try
            {
                var req = GetWebRequest(url, "POST");
                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                var bytePostData = Encoding.UTF8.GetBytes(postData);
                var reqStream = req.GetRequestStream();
                reqStream.Write(bytePostData, 0, bytePostData.Length);
                reqStream.Close();
                var rsp = (HttpWebResponse) req.GetResponse();
                if (string.IsNullOrWhiteSpace(rsp.CharacterSet))
                {
                    return GetResponseAsString(rsp, Encoding.UTF8);
                }
                var encoding = Encoding.GetEncoding(rsp.CharacterSet);
                return GetResponseAsString(rsp, encoding);
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }

        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> parameters)
        {
            try
            {
                var req = GetWebRequest(url, "POST");
                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                var postData = Encoding.UTF8.GetBytes(BuildQuery(parameters));
                var reqStream = req.GetRequestStream();
                reqStream.Write(postData, 0, postData.Length);

                reqStream.Close();
                //提交请求后，服务器返回的资源信息
                var rsp = (HttpWebResponse) req.GetResponse();
                if (string.IsNullOrWhiteSpace(rsp.CharacterSet))
                {
                    return GetResponseAsString(rsp, Encoding.UTF8);
                }
                var encoding = Encoding.GetEncoding(rsp.CharacterSet);
                return GetResponseAsString(rsp, encoding);
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
        }

        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public string DoGet(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters);
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters);
                }
            }

            var req = GetWebRequest(url, "GET");
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            var rsp = (HttpWebResponse)req.GetResponse();
            if (string.IsNullOrWhiteSpace(rsp.CharacterSet))
            {
                return GetResponseAsString(rsp, Encoding.UTF8);
            }
            var encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        /// <summary>
        /// 执行带文件上传的HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="fileParams">请求文件参数</param>
        /// <returns>HTTP响应</returns>
        public string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, FileItem> fileParams)
        {
            if (fileParams == null || fileParams.Count == 0)
            {
                return DoPost(url, textParams);
            }

            var boundary = DateTime.Now.Ticks.ToString("X"); 
            var req = GetWebRequest(url, "POST");
            req.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            var reqStream = req.GetRequestStream();
            var itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            var endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            const string textTemplate = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            var textEnum = textParams.GetEnumerator();
            while (textEnum.MoveNext())
            {
                var textEntry = string.Format(textTemplate, textEnum.Current.Key, textEnum.Current.Value);
                var itemBytes = Encoding.UTF8.GetBytes(textEntry);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);
            }
            const string fileTemplate = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            var fileEnum = fileParams.GetEnumerator();
            while (fileEnum.MoveNext())
            {
                var key = fileEnum.Current.Key;
                var fileItem = fileEnum.Current.Value;
                var fileEntry = string.Format(fileTemplate, key, fileItem.GetFileName(), fileItem.GetMimeType());
                var itemBytes = Encoding.UTF8.GetBytes(fileEntry);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);
                var fileBytes = fileItem.GetContent();
                reqStream.Write(fileBytes, 0, fileBytes.Length);
            }

            reqStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            reqStream.Close();

            var rsp = (HttpWebResponse)req.GetResponse();
            if(rsp.CharacterSet==null)
                throw new ArgumentNullException(nameof(rsp.CharacterSet));
            var encoding = Encoding.GetEncoding(rsp.CharacterSet);
            return GetResponseAsString(rsp, encoding);
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        { 
            return true;
        }


        /// <summary>
        /// 创建http请求
        /// </summary>
        /// <param name="url">传入的url</param>
        /// <param name="method">提交方式</param>
        /// <returns></returns>
        public HttpWebRequest GetWebRequest(string url, string method)
        {
            try
            {
                HttpWebRequest req;
                //根据请求的类型，创建WebRequest对象
                if (url.Contains("https"))
                {
                    //若请求为https，则需要验证服务器证书
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                    req = (HttpWebRequest) WebRequest.CreateDefault(new Uri(url));
                }
                else
                {
                    req = (HttpWebRequest) WebRequest.Create(url);
                }
                req.ServicePoint.Expect100Continue = false;
                req.Method = method;
                req.KeepAlive = true;
                req.UserAgent = "WeChat";
                req.Timeout = Timeout;
                return req;
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                stream = rsp.GetResponseStream();
                if (stream == null)
                    throw new ArgumentNullException(nameof(stream));
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
            finally
            {
                reader?.Close();
                stream?.Close();
                rsp?.Close();
            }
        }

        /// <summary>
        /// 组装GET请求URL。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>带参数的GET请求URL</returns>
        public string BuildGetUrl(string url, IDictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count <= 0) return url;
            if (url.Contains("?"))
            {
                url = url + "&" + BuildQuery(parameters);
            }
            else
            {
                url = url + "?" + BuildQuery(parameters);
            }
            return url;
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> parameters)
        {
            try
            {
                var postData = new StringBuilder();
                var hasParam = false;
                var dem = parameters.GetEnumerator();
                while (dem.MoveNext())
                {
                    var name = dem.Current.Key;
                    var value = dem.Current.Value;
                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) continue;
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    hasParam = true;
                }
                return postData.ToString();
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
        }


        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="saveDir"></param>
        /// <param name="saveFileName"></param>
        /// <param name="errHtml"></param>
        /// <returns></returns>
        private static bool  DownloadQrCode(string url, string saveDir, out string saveFileName, out string errHtml)
        {
            Stream outStream = null;
            WebClient wc = null;
            errHtml = string.Empty;
            if (!saveDir.EndsWith("\\"))
            {
                saveDir += "\\";
            }
            saveFileName = saveDir + Guid.NewGuid() + ".jpg";
            try
            {
                wc = new WebClient();
                var data = wc.DownloadData(url);
                if (File.Exists(saveFileName))
                    File.Delete(saveFileName);
                outStream = File.Create(saveFileName);
                foreach (var b in data)
                {
                    outStream.WriteByte(b);
                }
                return true;
            }
            catch (Exception ex)
            {
                errHtml = ex.Message;
                return false;
            }
            finally
            {
                wc?.Dispose();
                outStream?.Close();
            }
        }


        /// <summary>
        /// 创建多媒体消息请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="saveDir"></param>
        /// <param name="saveFileName"></param>
        /// <param name="errHtml"></param>
        /// <returns></returns>
        public bool DownloadFile(string url, string saveDir, out string saveFileName,out string errHtml)
        {
            if (url.IndexOf("showqrcode", StringComparison.Ordinal) != -1)
            {
                return DownloadQrCode(url, saveDir, out  saveFileName, out  errHtml);
            }
            saveFileName = string.Empty;
            errHtml = string.Empty;
            var isSuc = false;
            HttpWebResponse  response = null;
            try
            {
                var request = GetWebRequest(url, "GET");
                response = (HttpWebResponse)request.GetResponse();
                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    var disp = response.Headers.Get("Content-disposition");
                    var ext = disp.Substring(disp.LastIndexOf(".", StringComparison.Ordinal));
                    ext = ext.Substring(0, ext.Length - 1);
                    saveFileName = saveDir;
                    if (!saveFileName.EndsWith("\\"))
                    {
                        saveFileName += "\\";
                    }
                    saveFileName = saveFileName + Guid.NewGuid() + ext;
                    SaveBinaryFile(response, saveFileName);
                    isSuc = true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(response.CharacterSet))
                    {
                        errHtml = GetResponseAsString(response, Encoding.UTF8);
                    }
                    else
                    {
                        var encoding = Encoding.GetEncoding(response.CharacterSet);
                        errHtml = GetResponseAsString(response, encoding);
                    }
                }
            }
            finally
            {
                response?.Close();
            }
            return isSuc;
        }

        /// <summary>
        /// 将二进制文件保存到磁盘
        /// </summary>
        /// <param name="response">网络请求返回内容</param>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        private static bool SaveBinaryFile(WebResponse response, string fileName)
        {
            var value = true;
            var buffer = new byte[1024];
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
                Stream outStream = File.Create(fileName);
                var inStream = response.GetResponseStream();
                var l = 0;
                do
                {
                    if (inStream != null) l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                } while (l > 0);
                outStream.Close();
                inStream?.Close();
            }
            catch
            {
                value = false;
            }
            return value;
        }


        /// <summary>
        /// 根据Http请求返回的图片信息流，获取图片信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public  byte[] GetImage(string url)
        {
            if(string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            var request = GetWebRequest(url, "GET");
            MemoryStream memoryStream=new MemoryStream();
            using (HttpWebResponse resp=(HttpWebResponse)request.GetResponse())
            {
                var stream = resp.GetResponseStream();
                int k = 1024;
                var buff=new byte[k];
                while (k>0)
                {
                    if (stream==null)
                    {
                        return null;
                    }
                    k = stream.Read(buff, 0, 1024);
                    memoryStream.Write(buff,0,k);
                }
                memoryStream.Flush();
            }
           byte[] s= memoryStream.GetBuffer();
            //Bitmap img=new Bitmap(memoryStream);
            //return img;
            return s;
        }
    }
}
