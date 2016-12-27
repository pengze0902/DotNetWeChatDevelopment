using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatEntities
{
    /// <summary>
    /// 图文素材消息
    /// </summary>
   public class ReturnGraphicInterfaceMessage
    {
        /// <summary>
        /// 媒体类型
        /// </summary>
       public string Type { get; set; }

        /// <summary>
        /// 媒体文件上传后获取的唯一标识
        /// </summary>
       public string MediaId { get; set; }

        /// <summary>
        /// 媒体文件上传的时间
        /// </summary>
       public string CreateAt { get; set; }
    }
}
