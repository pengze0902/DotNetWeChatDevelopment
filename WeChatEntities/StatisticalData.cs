using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace WeChatEntities
{
   public class StatisticalData
    {
        /// <summary>
        /// 数据日期
        /// </summary>
       public string ref_date { get; set; }

        /// <summary>
        /// 用户渠道
        /// </summary>
       public int user_source { get; set; }

        /// <summary>
        /// 新增的用户数量
        /// </summary>
       public int new_user { get; set; }

        /// <summary>
        /// 取消关注的用户数量
        /// </summary>
       public int cancel_user { get; set; }

        /// <summary>
        /// 总用户量
        /// </summary>
       public int cumulate_user { get; set; }

    }

    public class StatisticalList
    {
        public List<StatisticalData> List { get; set; }
    }

}
