<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserGroup.aspx.cs" Inherits="WeChatAPI.Page.UserGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Button ID="CreateGroup" runat="server" Text="创建用户分组" OnClick="CreateGroup_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetGroups" runat="server" Text="查询所有分组" OnClick="GetGroups_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetUserGroupId" runat="server" Text="获取用户分组ID" OnClick="GetUserGroupId_Click" />&nbsp;&nbsp;&nbsp;
          <asp:Button ID="UserRemarkName" runat="server" Text="设置用户备注名" OnClick="UserRemarkName_Click" />          
        </div>
        <div style="margin-top: 20px;">
        <asp:Button ID="ModifyGroup" runat="server" Text="修改分组信息" OnClick="ModifyGroup_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SetUserGroup" runat="server" Text="移动用户分组" OnClick="SetUserGroup_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetUserInfo" runat="server" Text="获取用户基本信息" OnClick="GetUserInfo_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetAttentions" runat="server" Text="获取关注者列表" OnClick="GetAttentions_Click" />
        </div>
    </form>
</body>
</html>
