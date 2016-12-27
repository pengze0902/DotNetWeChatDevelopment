using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeChatAPI.DAL;

namespace WeChatAPI.Page
{
    public partial class MessageInterface : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadCreativeGraphicMessage_Click(object sender, EventArgs e)
        {
           Response.Write(MessageInterfaceOperation.UploadCreativeGraphicMessageMethod());
        }

        protected void BulkMessagePacket_Click(object sender, EventArgs e)
        {
            Response.Write(MessageInterfaceOperation.BulkMessagePacketMethod());
        }

        protected void OpenIdMessage_Click(object sender, EventArgs e)
        {
            Response.Write(MessageInterfaceOperation.OpenIdMessageMethod());
        }
    }
}