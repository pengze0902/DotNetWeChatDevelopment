using WeChatEntities;

namespace WeChatHelper.Response
{
    /// <summary>
    /// access_token请求回应信息
    /// </summary>
    public class AccessTokenGetResponse : MpResponse 
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public AccessTokenInfo AccessToken { get; set; }
    }
}
