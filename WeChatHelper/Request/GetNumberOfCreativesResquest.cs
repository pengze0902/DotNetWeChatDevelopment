using System.Collections.Generic;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatHelper.Request
{
    public class GetNumberOfCreativesResquest : RequestBase<GetNumberOfCreativesResponse>, IMpRequest<GetNumberOfCreativesResponse>
    {
        public string Method => "GET";

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
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/material/get_materialcount?access_token={0}";
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

        public GetNumberOfCreativesResponse ParseHtmlToResponse(string body)
        {
            var response = new GetNumberOfCreativesResponse { Body = body };
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                var numberOfCreatives = new NumberOfCreatives
                {
                    ImageCount = Tools.GetJosnValue(body, "image_count"),
                    NewsCounet = Tools.GetJosnValue(body, "news_count"),
                    VideoCount = Tools.GetJosnValue(body, "video_count"),
                    VoiceCount = Tools.GetJosnValue(body, "voice_count")
                };
                response.NumberOfCreatives = numberOfCreatives;
            }
            return response;
        }
    }
}
