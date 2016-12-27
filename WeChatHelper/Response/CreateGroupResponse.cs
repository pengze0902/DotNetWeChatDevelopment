using WeChatEntities;

namespace WeChatHelper.Response
{
    /// <summary>
    /// 创建分组响应信息
    /// </summary>
    public class CreateGroupResponse : MpResponse
    {
        /// <summary>
        /// 成功创建的分组信息
        /// </summary>
        public Group GroupInfo { get; set; }
    }
}
