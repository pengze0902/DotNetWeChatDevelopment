<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuOperation.aspx.cs" Inherits="WeChatAPI.Page.MenuOperation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>菜单操作</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="CreateMenu" runat="server" Text="创建菜单" OnClick="CreateMenu_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetMenu" runat="server" Text="查询菜单" OnClick="GetMenu_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="DeleteMenu" runat="server" Text="删除菜单" OnClick="DeleteMenu_Click" />
    </div>
    </form>
</body>
</html>
