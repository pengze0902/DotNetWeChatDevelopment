using System;

namespace WeChatEntities
{
    /// <summary>
    /// 数据统计Post发送数据
    /// </summary>
   public class Statistics
    {
        /// <summary>
        /// 统计其实时间
        /// </summary>
       public string BeginDate { get; set; }

        /// <summary>
        /// 统计截止时间
        /// </summary>
       public string EndDate { get; set; }

        /// <summary>
        /// 创建统计数据包
        /// </summary>
        /// <returns></returns>
        public string ToCreateJsonString()
        {
            var s = "{\"begin_date\":\"" + BeginDate + "\",\"end_date\":\"" + EndDate +"\"}";          
            return s;
        }
    }
}
