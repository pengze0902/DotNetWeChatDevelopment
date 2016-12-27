using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatHelper.Request
{
   public class SetUpUserRemarkName:RequestBase<GetUserInfoResponse>, IMpRequest<GetUserInfoResponse>
    {
        public string Method => "POST";

        /// <summary>
        /// 用户的OPENID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///分组id 
        /// </summary>
        public string ReamrkName { get; set; }

        /// <summary>
        /// 需要POST发送的数据
        /// </summary>
        public string SendData
        {
            get
            {
                var s = "{\"openid\":\"" + UserId + "\",\"remark\":" + ReamrkName + "}";
                return s;
            }
            set {  }
        }

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
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}";
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

        public GetUserInfoResponse ParseHtmlToResponse(string body)
        {
            var response = new GetUserInfoResponse { Body = body };
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                response.ErrMsg = Tools.GetJosnValue(body, "errmsg");
            }
            return response;
        }
    }
}
