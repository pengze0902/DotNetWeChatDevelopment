using System.Collections.Generic;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;

namespace WeChatHelper.Request
{
    /// <summary>
    /// 移动用户分组请求
    /// </summary>
    public class SetUserGroupRequest : RequestBase<SetUserGroupResponse>, IMpRequest<SetUserGroupResponse>
    {
        public string Method => "POST";

        /// <summary>
        /// 用户的OPENID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///分组id 
        /// </summary>
        public string ToGroupId { get; set; }

        /// <summary>
        /// 需要POST发送的数据
        /// </summary>
        public string SendData
        {
            get
            {
                var s = "{\"openid\":\"" + UserId + "\",\"to_groupid\":" + ToGroupId + "}";
                return s;
            }
            set
            {
            }
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
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";
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

        public SetUserGroupResponse ParseHtmlToResponse(string body)
        {
            var response = new SetUserGroupResponse { Body = body };

            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            return response;
        }
    }
}
