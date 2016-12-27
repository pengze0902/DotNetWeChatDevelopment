using System.Collections.Generic;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatHelper.Request
{
    public class GetCutBackStatisticsRequest : RequestBase<GetStatisticsResponse>, IMpRequest<GetStatisticsResponse>
    {
        public string Method => "POST";

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
        public AppIdInfo AppIdInfo { get; set; }

        /// <summary>
        /// 获取Api请求地址
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            //https://api.weixin.qq.com/datacube/getusercumulate?access_token=ACCESS_TOKEN 
            //const string urlFormat = "https://api.weixin.qq.com/datacube/getusersummary?access_token={0}";
            const string urlFormat = "https://api.weixin.qq.com/datacube/getusercumulate?access_token={0}";
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

        public GetStatisticsResponse ParseHtmlToResponse(string body)
        {
            var response = new GetStatisticsResponse{ Body = body };

            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                var jsonSerializer = new JavaScriptSerializer();
                var mg = jsonSerializer.Deserialize<StatisticalList>(body);
                response.StatisticalDataListResponse = mg;

            }
            return response;
        }
    }
}
