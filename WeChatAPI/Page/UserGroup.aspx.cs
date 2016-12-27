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
    public partial class UserGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateGroup_Click(object sender, EventArgs e)
        {
            var groupName = "测试分组";
            var createResponse = UserGroupOperation.CreateGroup(groupName);
            if (createResponse.IsError)
            {
                Response.Write("创建分组失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg);
            }
            else
            {
               Response.Write("创建分组成功，分组ID为：" + createResponse.GroupInfo.Id + "，分组名称为：" + createResponse.GroupInfo.Name);
            }
        }

        protected void GetGroups_Click(object sender, EventArgs e)
        {
            Response.Write(UserGroupOperation.GetGroups());
        }

        protected void GetUserGroupId_Click(object sender, EventArgs e)
        {
            string userId="";
            var response2 = UserGroupOperation.GetUserGroupId(userId);
            if (response2.IsError)
            {
               Response.Write("查询用户分组ID失败，错误信息为：" + response2.ErrInfo.ErrCode + "-" + response2.ErrInfo.ErrMsg);
            }
            else
            {
                Response.Write("查询用户分组ID成功，分组ID为：" + response2.GroupId);
            }
        }

        protected void ModifyGroup_Click(object sender, EventArgs e)
        {

            UserGroupOperation.ModifyGroup();
        }

        protected void SetUserGroup_Click(object sender, EventArgs e)
        {
            string userId = "";
            string groupId = "";
            UserGroupOperation.SetUserGroup(userId,groupId);
        }

        protected void GetUserInfo_Click(object sender, EventArgs e)
        {
            string openId = "oIXLKvpVPUpvQ7yR2mLSgNer-K5Y";
            Response.Write(UserGroupOperation.GetUserInfo(openId));
        }

        protected void GetAttentions_Click(object sender, EventArgs e)
        {
            Response.Write(UserGroupOperation.GetAttentions());
        }

        protected void UserRemarkName_Click(object sender, EventArgs e)
        {
            string s = "oIXLKvpVPUpvQ7yR2mLSgNer-K5Y";
            string ss = "测试帐号";
            var t = UserManagementOperation.RemarkName(s, ss);
            Response.Write(t);
        }
    }
}