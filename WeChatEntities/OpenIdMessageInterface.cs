using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatEntities
{
    /// <summary>
    /// 根据OpenID列表群发消息
    /// </summary>
    public class OpenIdMessageInterface
    {
        /// <summary>
        /// 填写图文消息接收者
        /// </summary>
        public string Touser { get; set; }

        /// <summary>
        /// 用于设定即将发送的图文消息
        /// </summary>
        public string Mpnews { get; set; }

        /// <summary>
        /// 用于群发的图文消息的MediaId
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 群发消息的类型
        /// </summary>
        public string Msgtype { get; set; }

        /// <summary>
        /// 消息的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体ID
        /// </summary>
        public string ThumbMediaId { get; set; }

        public string ToCreateJsonString()
        {
            var s = "{\"touser\": [" + Touser + "],\"mpnews\": {\"media_id\": \"" + MediaId + "\"}" + ",\"msgtype\":\"" + Msgtype + "\"}";
            return s;
        }
    }
}
