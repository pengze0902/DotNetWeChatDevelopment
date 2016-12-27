using WeChatEntities;

namespace WeChatHelper.Response
{
    /// <summary>
    /// 菜单查询请求回应信息
    /// </summary>
    public class GetMenuResponse : MpResponse
    {
        public MenuGroup Menu { get; set; }
    }
}
