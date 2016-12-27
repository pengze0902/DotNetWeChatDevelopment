using System.Collections.Generic;
using System.Text.RegularExpressions;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;
using Group = WeChatEntities.Group;


namespace WeChatHelper.Request
{
    /// <summary>
    /// 创建分组请求
    /// </summary>
    public class CreateGroupRequest : RequestBase<CreateGroupResponse>, IMpRequest<CreateGroupResponse>
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
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";
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

        public CreateGroupResponse ParseHtmlToResponse(string body)
        {
            //返回的json存入对象属性中
            var response = new CreateGroupResponse { Body = body };

            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                response.GroupInfo = new Group()
                {
                    //获取传入的json中节点的值
                    Id = Tools.GetJosnValue(body, "id"),
                    Name = Tools.GetJosnValue(body, "name")
                };
            }
            return response;
        }
    }
}
