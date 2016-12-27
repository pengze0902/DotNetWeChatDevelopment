using System.Collections.Generic;

namespace WeChatEntities
{
    /// <summary>
    /// 获取返回素材列表
    /// </summary>
    public class GetMateriaList
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图文消息的封面图片素材ID
        /// </summary>
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 图文消息的封面图片地址
        /// </summary>
        public string thumb_url { get; set; }

        /// <summary>
        /// 是否显示封面
        /// </summary>
        public string show_cover_pic { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 图文消息摘要
        /// </summary>
        public string digest { get; set; }

        /// <summary>
        /// 图文消息具体内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 图文页的URL
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 图文消息的原文地址
        /// </summary>
        public string content_source_url { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }

        public string ToJsonString()
        {
            var s = "{\"title\":\"" + title + "\",\"thumb_media_id\":\"" + thumb_media_id +
                "\",\"thumb_url\":\"" + thumb_url + "\",\"show_cover_pic\":\"" + show_cover_pic +
                "\",\"author\":\"" + author + "\",\"digest\":\"" + digest + "\",\"content\":\"" + content +
                "\",\"url\":\"" + url + "\",\"content_source_url\":\"" + content_source_url +
                 "\",\"name\":\"" + name + "\"}";
            return s;
        }
    }


    public class MateriaList
    {
        public List<GetMateriaList> MeLists { get; set; }

    }

    public class CreativeManagement
    {
        /// <summary>
        /// 该类型素材的总数
        /// </summary>
        public string total_count { get; set; }

        /// <summary>
        /// 本次调用获取的素材数量
        /// </summary>
        public string item_count { get; set; }

        public MateriaList news_item { get; set; }

        /// <summary>
        /// 图文消息素材的最后更新时间
        /// </summary>
        public string update_time { get; set; }

        public string ToJsonString()
        {
            var s1 = string.Empty;
            for (var i = 0; i < news_item.MeLists.Count; i++)
            {
                if (i > 0)
                {
                    s1 += ",";
                }
                s1 += news_item.MeLists[i].ToJsonString();
            }
            var s = "{\"total_count\":\"" + total_count + "\", \"item_count\":\"" + item_count +
                "\", \"news_item\":[" + s1 + "]" + "\",\"update_time\":\"" + update_time + "\"}";
            return s;
        }
    }
}
