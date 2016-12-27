using System.Collections.Generic;

namespace WeChatEntities
{
    /// <summary>
    /// 接口分析数据
    /// </summary>
   public class InterfaceAnalysis
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
        /// 通过服务器配置地址获得消息后，被动回复消息的次数
        /// </summary>
       public int call_back_count { get; set; }

        /// <summary>
        /// 上述动作的失败次数
        /// </summary>
       public int fail_count { get; set; }

        /// <summary>
        /// 总耗时
        /// </summary>
       public int total_time_cost { get; set; }

        /// <summary>
        /// 最大耗时
        /// </summary>
       public int max_time_cost { get; set; }
    }

    public class InterfaceAnalysisList
    {
        public List<InterfaceAnalysis> list { get; set; }
    }
}
