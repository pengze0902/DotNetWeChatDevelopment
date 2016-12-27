using System.Collections.Generic;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;

namespace WeChatHelper.Request
{
    /// <summary>
    /// 创建菜单请求
    /// </summary>
    public class CreateMenuRequest : RequestBase<CreateMenuResponse>, IMpRequest<CreateMenuResponse>
    {
        public string Method => "POST";

        /// <summary>
        /// 调用接口凭证 
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// AppId信息
        /// </summary>
        public AppIdInfo AppIdInfo { get; set; }

        /// <summary>
        /// 需要POST发送的数据
        /// </summary>
        public string SendData { get; set; }


        /// <summary>
        /// 获取Api请求地址
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
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

        public CreateMenuResponse ParseHtmlToResponse(string body)
        {
            var response = new CreateMenuResponse { Body = body };
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            return response;
        }
    }
}
