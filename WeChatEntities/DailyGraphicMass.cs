using System.Collections.Generic;

namespace WeChatEntities
{
    /// <summary>
    /// 每日群发图文统计
    /// </summary>
    public class DailyGraphicMass
    {
        /// <summary>
        /// 数据日期
        /// </summary>
        public string ref_date { get; set; }

        /// <summary>
        /// 数据的小时
        /// </summary>
        public int ref_hour { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        public string stat_date { get; set; }

        /// <summary>
        /// 图文ID+消息次序索引
        /// </summary>
        public string msg_id { get; set; }

        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图文页的阅读人数
        /// </summary>
        public int int_page_read_user { get; set; }

        /// <summary>
        /// 图文页的阅读次数
        /// </summary>
        public int int_page_read_count { get; set; }

        /// <summary>
        /// 原文页的阅读人数，无原文页时此数据为0
        /// </summary>
        public int ori_page_read_user { get; set; }

        /// <summary>
        /// 原文的阅读次数
        /// </summary>
        public int ori_page_read_count { get; set; }

        /// <summary>
        /// 分享场景
        /// </summary>
        public int share_scence { get; set; }

        /// <summary>
        /// 分享人数
        /// </summary>
        public int share_user { get; set; }

        /// <summary>
        /// 分享次数
        /// </summary>
        public int share_count { get; set; }

        /// <summary>
        /// 收藏人数
        /// </summary>
        public int add_to_fav_user { get; set; }

        /// <summary>
        /// 收藏的次数
        /// </summary>
        public int add_to_fav_count { get; set; }

        /// <summary>
        /// 送达人数
        /// </summary>
        public int target_user { get; set; }

        /// <summary>
        /// 在获取图文阅读分时才有该字段，代表用户从哪里进入阅读该图文
        /// </summary>
        public int user_source { get; set; }
    }

    public class DailyGraphicMassList
    {
        public List<DailyGraphicMass> list { get; set; }
    }
}
