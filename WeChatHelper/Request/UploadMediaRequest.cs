using System.Collections.Generic;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatHelper.Request
{
    /// <summary>
    /// 上传多媒体文件请求
    /// </summary>
    public class UploadMediaRequest : RequestBase<UploadMediaResponse>, IMpRequest<UploadMediaResponse>
    {
        public string Method => "POST";

        /// <summary>
        /// 调用接口凭证 
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 多媒体文件名
        /// </summary>
        public string FileName { get; set; }

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
            const string urlFormat = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            var url = string.Format(urlFormat, AccessToken, Type);
            return url;
        }

        public IDictionary<string, string> GetParameters()
        {
            return new Dictionary<string, string>();
        }

        public void Validate()
        {

        }

        public UploadMediaResponse ParseHtmlToResponse(string body)
        {
            var response = new UploadMediaResponse { Body = body };
            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                response.Type = Tools.GetJosnValue(body, "type");
                response.MediaId = Tools.GetJosnValue(body, "media_id");
                if (string.IsNullOrWhiteSpace(response.MediaId))
                {
                    response.MediaId = Tools.GetJosnValue(body, "thumb_media_id");
                }
                response.CreatedAt = Tools.GetJosnValue(body, "created_at");
            }
            return response;
        }


    }
}
