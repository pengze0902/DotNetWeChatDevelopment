using System;
using System.Collections.Generic;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Util;

namespace WeChatHelper
{
    /// <summary>
    /// 微信公众平台客户端
    /// </summary>
    public class MpClient : IMpClient
    {
        private WebUtils _webUtils;

        /// <summary>
        /// 执行微信公众平台API请求
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的微信公众平台请求</param>
        /// <returns>领域对象</returns>
        public T Execute<T>(IMpRequest<T> request) where T : MpResponse
        {
            request.Validate();
            string body;
            _webUtils = new WebUtils();
            var url = request.GetReqUrl();
            if (request.Method.Equals("GET", StringComparison.InvariantCultureIgnoreCase))
            {
                if (request.GetType().ToString() == "WeChatHelper.Request.DownloadMediaRequest")
                {
                    var fileName = string.Empty;
                    var errHtml = string.Empty;
                    var downloadMediaRequest = request as DownloadMediaRequest;
                    var isSuc = downloadMediaRequest != null && _webUtils.DownloadFile(url, downloadMediaRequest.SaveDir, out fileName, out errHtml);
                    body = isSuc ? fileName : errHtml;
                }
                else
                {
                    body = _webUtils.DoGet(url, null);
                }
            }
            else
            {
                if (request.GetType().ToString() == "WeChatHelper.Request.UploadMediaRequest")
                {
                    var files = new Dictionary<string, FileItem>();
                    var uploadMediaRequest = request as UploadMediaRequest;
                    if (uploadMediaRequest != null)
                    {
                        var fileItem = new FileItem(uploadMediaRequest.FileName);
                        files.Add(Guid.NewGuid().ToString(), fileItem);
                    }
                    body = _webUtils.DoPost(url, request.GetParameters(), files);
                }
                else
                {
                    body = _webUtils.DoPost(url, request.SendData);
                }
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                return null;
            }
            //微信服务器返回的字符串内容body，request传入的对象，并获取对应对象的方法
            var response = request.ParseHtmlToResponse(body);

            return response;
        }
    }
}
