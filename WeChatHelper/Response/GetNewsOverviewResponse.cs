using WeChatEntities;

namespace WeChatHelper.Response
{
   public class GetNewsOverviewResponse:MpResponse
    {
       public NewsOverviewList NewsOverview { get; set; }
    }
}
