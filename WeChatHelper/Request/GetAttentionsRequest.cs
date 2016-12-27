using System.Collections.Generic;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;

namespace WeChatHelper.Request
{
    /// <summary>
    /// 获取关注者列表请求
    /// </summary>
    public class GetAttentionsRequest : RequestBase<GetAttentionsResponse>, IMpRequest<GetAttentionsResponse>
    {
        public string Method => "GET";

        /// <summary>
        /// 第一个拉取的OPENID，不填默认从头开始拉取 
        /// </summary>
        public string NextOpenId { get; set; }

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
        public AppIdInfo AppIdInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 获取Api请求地址
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}";
            var url = string.Format(urlFormat, AccessToken);
            if (!string.IsNullOrWhiteSpace(NextOpenId))
            {
                url += "&next_openid=" + NextOpenId;
            }
            return url;
        }

        public IDictionary<string, string> GetParameters()
        {
            return new Dictionary<string, string>();
        }

        public void Validate()
        {

        }

        public GetAttentionsResponse ParseHtmlToResponse(string body)
        {
            var response = new GetAttentionsResponse {Body = body};
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                var jsonSerializer = new JavaScriptSerializer();
                var atts = jsonSerializer.Deserialize<Attentions>(body);
                response.Attentions = atts;
            }
            return response;
        }
    }
}
