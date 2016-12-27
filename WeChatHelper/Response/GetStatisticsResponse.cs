using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatEntities;

namespace WeChatHelper.Response
{
    /// <summary>
    /// 接收用户分析数据
    /// </summary>
   public class GetStatisticsResponse:MpResponse
    {
       public StatisticalList StatisticalDataListResponse { get; set; }
    }
}
