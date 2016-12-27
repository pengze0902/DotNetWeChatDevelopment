using System.Collections.Generic;

namespace WeChatEntities
{
    /// <summary>
    /// 关注者列表
    /// </summary>
    public class Attentions
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public string NextOpenid { get; set; }
        public AttentionsData Data { get; set; }
    }

    /// <summary>
    /// 关注者列表数据
    /// </summary>
    public class AttentionsData
    {
        public List<string> Openid { get; set; }
    }
}

