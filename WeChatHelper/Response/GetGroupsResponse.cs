using WeChatEntities;

namespace WeChatHelper.Response
{
    /// <summary>
    /// 查询所有分组回应信息
    /// </summary>
    public class GetGroupsResponse : MpResponse
    {
        /// <summary>
        /// 分组列表信息
        /// </summary>
        public Groups Groups { get; set; }
    }
}
