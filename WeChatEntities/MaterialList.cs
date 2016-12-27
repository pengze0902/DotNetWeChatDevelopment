namespace WeChatEntities
{
    /// <summary>
    /// 获取素材列表请求参数
    /// </summary>
    public class MaterialList
    {
        /// <summary>
        /// 素材类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 从全部素材的该偏移位置开始返回
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// 返回素材的数量
        /// </summary>
        public int Count { get; set; }

        public string ToJsonString()
        {
            var s = "{\"type\":\"" + Type + "\", \"offest\":\"" + Offset + "\",\"count\":\"" + Count + "\"}";
            return s;
        }
    }
}
