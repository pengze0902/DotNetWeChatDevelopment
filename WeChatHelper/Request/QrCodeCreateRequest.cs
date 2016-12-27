using System;
using System.Collections.Generic;
using System.Threading;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatHelper.Request
{
    /// <summary>
    /// 创建二维码请求
    /// </summary>
    public class QrCodeCreateRequest : RequestBase<QrCodeCreateResponse>, IMpRequest<QrCodeCreateResponse>
    {
        public string Method => "POST";

        /// <summary>
        /// 创建二维码消息
        /// </summary>
        public QrCodeCreateMessage QrCodeCreateMessage { get; set; }

        /// <summary>
        /// 需要POST发送的数据
        /// </summary>
        public string SendData { get; set; }

        /// <summary>
        /// 调用接口凭证 
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// AppId信息
        /// </summary>
        public AppIdInfo AppIdInfo { get; set; }

        /// <summary>
        /// 获取Api请求地址
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";
            var url = string.Format(urlFormat, AccessToken);
            return url;
        }

        public IDictionary<string, string> GetParameters()
        {
            return new Dictionary<string, string>();
        }

        public void Validate()
        {

        }

        public QrCodeCreateResponse ParseHtmlToResponse(string body)
        {
            var response = new QrCodeCreateResponse { Body = body };
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                response.Ticket = Tools.GetJosnValue(body, "ticket");
                var exp = Tools.GetJosnValue(body, "expire_seconds");
                if (!string.IsNullOrWhiteSpace(exp))
                {
                    response.ExpireSeconds = Convert.ToInt32(exp);
                }
                if (string.IsNullOrWhiteSpace(this.QrCodeCreateMessage.LocalStoredDir)) return response;
                var webUtils = new WebUtils();
                string fileName;
                string errMsg;
                Thread.Sleep(3000);
                //暂时下载不了文件，可能腾讯限制了
                if (!webUtils.DownloadFile(response.QrCodeUrl, QrCodeCreateMessage.LocalStoredDir, out fileName, out errMsg))
                {
                    response.ErrInfo = new ErrInfo()
                    {
                        ErrCode = -9999,
                        ErrMsg = "创建二维码请求成功，但是下载到本地失败"
                    };
                }
            }
            return response;
        }
    }
}
