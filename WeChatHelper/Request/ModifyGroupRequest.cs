using System.Collections.Generic;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;

namespace WeChatHelper.Request
{
    /// <summary>
    /// 修改分组信息请求
    /// </summary>
    public class ModifyGroupRequest : RequestBase<ModifyGroupResponse>, IMpRequest<ModifyGroupResponse>
    {
        public string Method => "POST";

        /// <summary>
        /// 要修改的分组
        /// </summary>
        public Group GroupInfo { get; set; }

        /// <summary>
        /// 需要POST发送的数据
        /// </summary>
        public string SendData
        {
            get;
            set;
        }

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
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}";
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

        public ModifyGroupResponse ParseHtmlToResponse(string body)
        {
            var response = new ModifyGroupResponse {Body = body};
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            return response;
        }
    }
}
