namespace WeChatHelper.Response
{
    /// <summary>
    /// 通过用户的OpenID查询其所在的GroupID回应消息
    /// </summary>
    public class GetUserGroupResponse : MpResponse
    {
        /// <summary>
        /// 返回的用户分组ID
        /// </summary>
        public string GroupId { get; set; }
    }
}
