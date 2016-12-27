using WeChatEntities;

namespace WeChatHelper.Response
{
    /// <summary>
    /// 获取关注者列表请求回应信息
    /// </summary>
    public class GetAttentionsResponse : MpResponse 
    {
        /// <summary>
        /// 关注者列表数据
        /// </summary>
        public Attentions Attentions { get; set; }
    }
}
