using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeChatAPI.DAL;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;

namespace WeChatAPI.Page
{
    public partial class DeleteMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           var delResponse = MenuOperation.DeleteMenuMethod();
            if (delResponse.IsError)
            {
               Response.Write(string.Format("删除菜单失败，错误信息为：" + delResponse.ErrInfo.ErrCode + "-" + delResponse.ErrInfo.ErrMsg));
            }
            else
            {
                HttpContext.Current.Response.Write("删除菜单成功");

               MenuOperation.GetMenuMethod(); //成功后查询一下
            }
        }


    }
}