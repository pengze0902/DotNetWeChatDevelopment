using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatHelper.Request
{
   public class OpenIdMessageInterfaceRequest:RequestBase<OpenIdMessageInterfaceResponse>,IMpRequest<OpenIdMessageInterfaceResponse>
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
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";
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

        public OpenIdMessageInterfaceResponse ParseHtmlToResponse(string body)
        {
            var response = new OpenIdMessageInterfaceResponse { Body = body };
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            ReturnOpenIdMessageInterface returnGraphicInterfaceMessage = new ReturnOpenIdMessageInterface
            {
                Type = Tools.GetJosnValue(body, "type"),
                Errcode = Tools.GetJosnValue(body, "errcode"),
                Errmsg = Tools.GetJosnValue(body, "errmsg"),
                MsgId = Tools.GetJosnValue(body, "msg_id"),
                MsgDataId = Tools.GetJosnValue(body, "msg_data_id"),
            };
            response.ReturnOpenIdMessageInterface = returnGraphicInterfaceMessage;
            return response;
        }
    }
}
