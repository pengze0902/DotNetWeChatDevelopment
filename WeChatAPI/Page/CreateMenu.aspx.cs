using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WeChatAPI.DAL;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;

namespace WeChatAPI.Page
{
    public partial class CreateMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           var createResponse= MenuOperation.CreateMenuMethod();
            if (createResponse.IsError)
            {
                Response.Write(string.Format("创建菜单失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg));
            }
            else
            {
                Response.Write("创建菜单成功");
            }
        }

    }
}