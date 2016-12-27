namespace WeChatEntities
{
    /// <summary>
    /// access_token实体类
    /// 公众号可以使用AppID和AppSecret调用本接口来获取access_token
    /// </summary>
    public class AccessTokenInfo
    {
        /// <summary>
        /// 公众号的全局唯一票据
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒 
        /// </summary>
        public long ExpiresIn { get; set; }
    }
}

