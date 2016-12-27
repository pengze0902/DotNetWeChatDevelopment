<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreativeManagement.aspx.cs" Inherits="WeChatAPI.Page.CreativeManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="NewCreativeTemporary" runat="server" Text="新增临时素材" OnClick="NewCreativeTemporary_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetCreativeTemporary" runat="server" Text="获取临时素材" OnClick="GetCreativeTemporary_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="NewPermanentMaterial" runat="server" Text="新增永久素材" OnClick="NewPermanentMaterial_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetPermanentMaterial" runat="server" Text="获取永久素材" OnClick="GetPermanentMaterial_Click" />
        </div>
        <div>
        <asp:Button ID="PermanentlyDeletedMaterial" runat="server" Text="删除永久素材" OnClick="PermanentlyDeletedMaterial_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ModifyPermanentGraphicMaterial" runat="server" Text="修改永久图文素材" OnClick="ModifyPermanentGraphicMaterial_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetsTotalCreative" runat="server" Text="获取素材总数" OnClick="GetsTotalCreative_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetCreativeList" runat="server" Text="获取素材列表" OnClick="GetCreativeList_Click" />
    </div>
        <div>
                       </div>
    </form>
</body>
</html>
