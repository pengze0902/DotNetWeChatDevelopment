using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeChatAPI.DAL;

namespace WeChatAPI.Page
{
    public partial class MenuOperation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateMenu_Click(object sender, EventArgs e)
        {
            var createResponse = MenuOperationMethod.CreateMenuMethod();
            if (createResponse.IsError)
            {
                Response.Write(string.Format("创建菜单失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg));
            }
            else
            {
                Response.Write("创建菜单成功");
            }

        }
        
        protected void GetMenu_Click(object sender, EventArgs e)
        {
            var getResponse = MenuOperationMethod.GetMenuMethod();
            if (getResponse.IsError)
            {
                Response.Write(string.Format("查询菜单发生错误，错误信息为：" + getResponse.ErrInfo.ErrCode + "-" + getResponse.ErrInfo.ErrMsg));
            }
            else
            {
                Response.Write("查询菜单成功");
                var m = getResponse.Menu;
                Response.Write(m.ToJsonString());
            }
        }

        protected void DeleteMenu_Click(object sender, EventArgs e)
        {
            var delResponse = MenuOperationMethod.DeleteMenuMethod();
            if (delResponse.IsError)
            {
                Response.Write(string.Format("删除菜单失败，错误信息为：" + delResponse.ErrInfo.ErrCode + "-" + delResponse.ErrInfo.ErrMsg));
            }
            else
            {
                HttpContext.Current.Response.Write("删除菜单成功");

                MenuOperationMethod.GetMenuMethod(); //成功后查询一下
            }
        }
    }
}