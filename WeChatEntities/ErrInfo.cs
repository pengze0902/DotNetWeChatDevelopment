namespace WeChatEntities
{
    /// <summary>
    /// 微信公众平台接口调用返回码以及返回信息实体类
    /// </summary>
    public class ErrInfo
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public long ErrCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string ErrMsg { get; set; }
    }
}

