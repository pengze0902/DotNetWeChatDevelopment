using System;
using WeChatAPI.DAL;

namespace WeChatAPI.Page
{
    public partial class GetMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        var getResponse=MenuOperation.GetMenuMethod();
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
    }
}