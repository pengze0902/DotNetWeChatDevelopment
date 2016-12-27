using WeChatEntities;

namespace WeChatHelper.Response
{
    /// <summary>
    /// 获取用户基本信心请求回应信息
    /// </summary>
    public class GetUserInfoResponse : MpResponse
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public User UserInfo { get; set; }
    }
}
