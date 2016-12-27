using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatEntities
{
    public class PreviewMessageInterface
    {
        public string Touser { get; set; }

        public string Msgtype { get; set; }

        public string MedisId { get; set; }

        public string Content { get; set; }
    }
}
