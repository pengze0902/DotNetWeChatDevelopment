using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WeChatEntities
{
    /// <summary>
    /// 永久素材
    /// </summary>
   public class PermanentMaterial
    {

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图文消息的封面素材ID
        /// </summary>
       public string thumb_media_id { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
       public string author { get; set; }

        /// <summary>
        /// 图文消息的摘要
        /// </summary>
       public string digest { get; set; }

        /// <summary>
        /// 是否显示封面
        /// </summary>
       public int show_cover_pic { get; set; }

        /// <summary>
        /// 图文消息的具体内容
        /// </summary>
       public string content { get; set; }

        /// <summary>
        /// 图文消息的原文地址
        /// </summary>
        public string content_source_url { get; set; }


        public string ToJsonString()
        {
            var s = "{\"title\":\"" + title + "\", \"thumb_media_id\":\"" + thumb_media_id + 
                "\",\"author\":\"" + author + "\",\"digest\":\"" + digest + 
                "\",\"show_cover_pic\":\"" + show_cover_pic + "\",\"content\":\"" + content + 
                "\",\"content_source_url\":\"" + content_source_url + "\"}";
            return s;
        }
    }

    public class PermanentMaterialList
    {
        public List<PermanentMaterial> Permanent { get; set; }    
    }

    public class Material
    {
        public PermanentMaterialList articles { get; set; }

        public string ToJsonString()
        {
            var s1 = string.Empty;
            for (var i = 0; i < articles.Permanent.Count; i++)
            {
                if (i > 0)
                {
                    s1 += ",";
                }
                s1 += articles.Permanent[i].ToJsonString();
            }
            var s = "{ \"articles\":[" + s1 + "]}";
            return s;
        }
    }
}
