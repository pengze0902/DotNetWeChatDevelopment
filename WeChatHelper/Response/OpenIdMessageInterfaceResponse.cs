using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatEntities;

namespace WeChatHelper.Response
{
   public class OpenIdMessageInterfaceResponse:MpResponse
    {
       public ReturnOpenIdMessageInterface ReturnOpenIdMessageInterface { get; set; }
    }
}
