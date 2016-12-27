using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatEntities
{
    /// <summary>
    /// 根据分组进行群发
    /// </summary>
    public class PacketInterfaceMessage
    {
        /// <summary>
        /// 用于设定是否向全部用户发送
        /// </summary>
        public string IsToAll { get; set; }

        /// <summary>
        /// 群发到分组的ID
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 用于群发的消息
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 群发消息类型
        /// </summary>
        public string Msgtype { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 视频缩略图的媒体ID
        /// </summary>
        public string ThumbMediaId { get; set; }

        /// <summary>
        /// 推送不同类型的消息时，需修改image处
        /// </summary>
        /// <returns></returns>
        public string ToCreateJsonString()
        {
            var s = "{\"filter\": {\"is_to_all\": " + IsToAll + ",\"group_id\":" + GroupId + "}"
                    + ",\"mpnews\": {\"media_id\": \"" + MediaId + "\"}"+ ",\"msgtype\":\"" + Msgtype + "\"}";
            return s;
        }
    }
}


