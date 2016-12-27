using System;
using System.Collections.Generic;

namespace WeChatEntities
{
    /// <summary>
    /// 消息概况
    /// </summary>
    public class NewsOverview
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }

        /// <summary>
        /// 数据的小时
        /// </summary>
        public int ref_hour { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int msg_type { get; set; }

        /// <summary>
        /// 上行发送了消息的用户数
        /// </summary>
        public int msg_user { get; set; }

        /// <summary>
        /// 上行发送了消息的消息总数
        /// </summary>
        public int msg_count { get; set; }

        /// <summary>
        /// 当日发送消息量分布的区间
        /// </summary>
        public int count_interval { get; set; }

        /// <summary>
        /// 图文页的阅读次数
        /// </summary>
        public int int_page_read_count { get; set; }

        /// <summary>
        /// 原文页的阅读人数，无原文页时此数据为0
        /// </summary>
        public int ori_page_read_user { get; set; }
    }

    public class NewsOverviewList
    {
        public List<NewsOverview> list { get; set; }
    }
}
