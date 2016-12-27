using System.Collections.Generic;

namespace WeChatEntities
{
    /// <summary>
    /// 上传图文消息素材
    /// </summary>
    public class GraphicInterfaceMessage
    {
        /// <summary>
        /// 图文消息缩略图
        /// </summary>
        public string ThumbMediaId { get; set; }

        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 在图文哦消息页面点击“阅读原文”后的页面
        /// </summary>
        public string ContentSourceUrl { get; set; }

        /// <summary>
        /// 图文消息页面内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string Digest { get; set; }

        /// <summary>
        /// 是否显示封面
        /// </summary>
        public string ShowCoverPic { get; set; }

        public string ToJsonString()
        {
            var s = "{\"thumb_media_id\":\"" + ThumbMediaId +
                "\",\"author\":\"" + Author + "\",\"title\":\"" + Title +
                "\",\"content_source_url\":\"" + ContentSourceUrl + "\",\"content\":\"" + Content + "\",\"digest\":\"" + Digest +
                "\",\"show_cover_pic\":\"" + ShowCoverPic +"\"}";
            return s;
        }
    }

    public class GraphicInterfaceMessageList
    {
        public List<GraphicInterfaceMessage> GraphicInterfaceMessagesList { get; set; }
    }

    public class InterfaceMessageList
    {
        public GraphicInterfaceMessageList GraphicInterface { get; set; }

        public string ToJsonString()
        {
            var s1 = string.Empty;
            for (var i = 0; i < GraphicInterface.GraphicInterfaceMessagesList.Count; i++)
            {
                if (i > 0)
                {
                    s1 += ",";
                }
                s1 += GraphicInterface.GraphicInterfaceMessagesList[i].ToJsonString();
            }
            var s = "{\"articles\":[" + s1 + "]" + "\"}";
            return s;
        }
    }
}
