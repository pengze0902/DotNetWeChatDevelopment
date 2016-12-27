using System.Collections.Generic;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatHelper.Request
{
   public class GraphicInterfaceMessageRequest:RequestBase<ReturnGraphicInterfaceMessageResponse>,IMpRequest<ReturnGraphicInterfaceMessageResponse>
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
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
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

        public ReturnGraphicInterfaceMessageResponse ParseHtmlToResponse(string body)
        {
            var response = new ReturnGraphicInterfaceMessageResponse { Body = body };
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            ReturnGraphicInterfaceMessage returnGraphicInterfaceMessage = new ReturnGraphicInterfaceMessage
            {
                Type = Tools.GetJosnValue(body,"type"),
                MediaId = Tools.GetJosnValue(body,"media_id"),
                CreateAt = Tools.GetJosnValue(body,"created_at")
            };
            response.ReturnGraphicInterfaceMessage = returnGraphicInterfaceMessage;
            return response;
        }
    }
}
