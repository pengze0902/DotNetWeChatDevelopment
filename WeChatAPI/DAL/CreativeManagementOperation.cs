using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Response;

namespace WeChatAPI.DAL
{
    public static class CreativeManagementOperation
    {
        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";
        public static string NewCreativeTemporaryMethod()
        {
            string imgPath = "D:/Image/WeChatImage.jpg";
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }

            var requestUpload = new UploadMediaRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                Type = "image",
                FileName = imgPath
            };
            var createResponse = mpClient.Execute(requestUpload);
            if (createResponse.IsError)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                return createResponse.ErrInfo.ErrMsg;
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(createResponse.Body);
        }

        public static string GetCreativeTemporaryMethod()
        {
            string MediaId = "7udfbNhsspUAm-y_TGbNBE6liHH7G1WyZefiTiz4Q9dJ_zJQmHh9nHQg46dK1FTG";
            string SaveDir = "D:/upload";
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }

            var requestUpload = new DownloadMediaRequest
            {
                AccessToken = response.AccessToken.AccessToken,
                MediaId = MediaId,
                SaveDir = SaveDir
            };
            var createResponse = mpClient.Execute(requestUpload);
            if (createResponse.IsError)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                return createResponse.ErrInfo.ErrMsg;
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(createResponse.Body);
        }

        public static string NewPermanentMaterialMethod()
        {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }
            string path = "D:/Image/WeChatImage.jpg";
            string filename = "WeChatImage.jpg";
            string type = "image";
            #region
            //FileStream fileStream =new FileStream(path,FileMode.Open,FileAccess.Read);
            //long fileLength = fileStream.Length;
            //string s =
            //    string.Format(
            //        "Content-Disposition:form-data;name=\"media\";filename=\"{0}\";filelength=\"{1}\"\r\n Content-Type:application/octet-stream\r\n\r\n",
            //        filename, fileLength);
            //StringBuilder stringBuilder=new StringBuilder();
            //stringBuilder.AppendLine("{");
            //stringBuilder.AppendLine("\"type\":\"image\",");
            //stringBuilder.AppendLine("\"media\":\"" + s + "\"");
            //stringBuilder.AppendLine("}");


            //            Dictionary<string,string> fileDictionary=new Dictionary<string, string>();
            //            MemoryStream memoryStream=new MemoryStream();

            //            //是否用Form上传文件
            //            var formUploadFile = fileDictionary != null && fileDictionary.Count > 0;

            //            if (formUploadFile)
            //            {
            //                string bounday = "----" + DateTime.Now.Ticks.ToString("*");
            //                string formdataTemplate = "\r\n--" + bounday +
            //                                          "\r\nContent-Disposition:form-data;name=\"media\";filename=\"{0}\";filelength=\"{1}\"\r\n Content-Type:application/octet-stream\r\n\r\n";
            //                foreach (var file in fileDictionary)
            //                {
            //                    try
            //                    {
            //                        using (FileStream fStream=new FileStream(path,FileMode.Open,FileAccess.Read))
            //                        {
            //                            var formdata = string.Format(formdataTemplate, file.Key, filename);
            //                            var formdataBytes =
            //                                Encoding.ASCII.GetBytes(memoryStream.Length == 0
            //                                    ? formdata.Substring(2, formdata.Length - 2)
            //                                    : formdata);
            //                            memoryStream.Write(formdataBytes,0,formdataBytes.Length);
            //                            byte[] buffer=new byte[1024];
            //                            int bytesRead = 0;
            //                            while ((bytesRead= fStream.Read(buffer, 0, buffer.Length))!=0)
            //                            {
            //                                memoryStream.Write(buffer,0,bytesRead);
            //                            }
            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        throw new Exception(ex.Message);
            //                    }

            //                    var footer = Encoding.ASCII.GetBytes("\r\n--" + bounday + "--\r\n");

            //                }
            //            }
            #endregion

            string urlFormat = "https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}"; 
            var url = string.IsNullOrEmpty(response.AccessToken.AccessToken) ? urlFormat : string.Format(urlFormat, response.AccessToken.AccessToken);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("media",path);
            var imageResponse = UploadCreativeManagement.UploadCreative(url, dictionary);
            PermanentMaterial permanent = new PermanentMaterial
            {
                title = "测试",
                thumb_media_id = imageResponse,
                author = "ceshi",
                digest = "ceshitupian",
                show_cover_pic = 1,
                content = "这是一个测试图文的例子",
                content_source_url = "https://www.baidu.com"
            };

            List<PermanentMaterial> list = new List<PermanentMaterial> { permanent };

            PermanentMaterialList meList = new PermanentMaterialList { Permanent = list };

            Material meMaterial = new Material
            {
                articles = meList
            };

           var s = meMaterial.ToJsonString();
            var requestUpload = new NewsPermanentMaterialRequest
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = s
            };
            var createResponse = mpClient.Execute(requestUpload);
            if (createResponse.IsError)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                return createResponse.ErrInfo.ErrMsg;
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(createResponse.Body);
        }

        public static string NumberOfCreativesMethod()
        {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }

            var requestUpload = new GetNumberOfCreativesResquest
            {
                AccessToken = response.AccessToken.AccessToken,
            };
            var createResponse = mpClient.Execute(requestUpload);
            if (createResponse.IsError)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                return createResponse.ErrInfo.ErrMsg;
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(createResponse.Body);
        }


        public static string GetCreativeList()
        {
            string type = "news";
            int offset = 0;
            int count = 1;
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }

            MaterialList materialList = new MaterialList
            {
                Type = type,
                Offset = offset,
                Count = count
            };

            var requestUpload = new GetMaterialListRequest
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = materialList.ToJsonString()
            };
            var createResponse = mpClient.Execute(requestUpload);
            if (createResponse.IsError)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                return createResponse.ErrInfo.ErrMsg;
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(createResponse.Body);
        }



        //public static string ImageMedia(string path)
        //{
        //    FileStream fileStream = new FileStream(path, FileMode.Open);
        //    string stream = new StreamReader(fileStream).ReadToEnd();
        //    MemoryStream memoryStream = new MemoryStream();
        //    string fileHeaderName = "media";
        //    string headerTemplate =
        //        "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\n Content-Type:application/octet-stream\r\n\r\n";
        //    for (int i = 0; i < stream.Length; i++)
        //    {
        //        string header = string.Format(headerTemplate, fileHeaderName, stream[i]);
        //        byte[] headerBytes = Encoding.Unicode.GetBytes(header);
        //        memoryStream.Write(headerBytes, 0, headerBytes.Length);
        //        FileStream files = new FileStream(stream[i], FileMode.Open, FileAccess.Read);
        //        byte[] buff = new byte[1024];
        //        int bytesRead = 0;
        //        while ((bytesRead = fileStream.Read(buff, 0, buff.Length)) != 0)
        //        {
        //            memoryStream.Write(buff, 0, bytesRead);
        //        }
        //        memoryStream.Write();
        //    }
        //}

      
    }
}