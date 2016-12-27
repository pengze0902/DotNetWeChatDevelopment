using System;
using System.Collections.Generic;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;


namespace WeChatHelper.Request
{
    /// <summary>
    /// 获取access_token请求
    /// </summary>
    public class AccessTokenGetRequest : RequestBase<AccessTokenGetResponse>, IMpRequest<AccessTokenGetResponse>
    {
        public string Method => "GET";

        /// <summary>
        /// AppId信息
        /// </summary>
        public AppIdInfo AppIdInfo { get; set; }
        ///<summary>
        /// 需要POST发送的数据
        /// </summary>
        public string SendData { get; set; }

        /// <summary>
        /// 获取Api请求地址
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}";
            var info = AppIdInfo ?? GetDefaultAppIdInfo();
            var url = string.Format(urlFormat, "client_credential", info.AppId, info.AppSecret);
            return url;
        }

        public IDictionary<string, string> GetParameters()
        {
            return null;
        }

        public void Validate()
        {

        }

        /// <summary>
        /// 将平台返回的HTML转化成MpResponse对象
        /// </summary>
        /// <param name="body">返回的HTML</param>
        /// <returns>公众号的全局唯一票据</returns>
        public AccessTokenGetResponse ParseHtmlToResponse(string body)
        {
            var response = new AccessTokenGetResponse { Body = body };
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                var token = new AccessTokenInfo()
                {
                    AccessToken = Tools.GetJosnValue(body, "access_token"),
                    ExpiresIn = Convert.ToInt64(Tools.GetJosnValue(body, "expires_in"))
                };
                response.AccessToken = token;
            }
            return response;
        }
    }
}
