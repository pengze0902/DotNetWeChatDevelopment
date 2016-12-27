<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageInterface.aspx.cs" Inherits="WeChatAPI.Page.MessageInterface" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="UploadCreativeGraphicMessage" runat="server" Text="上传图文消息素材" OnClick="UploadCreativeGraphicMessage_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BulkMessagePacket" runat="server" Text="分组群发消息" OnClick="BulkMessagePacket_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="OpenIdMessage" runat="server" Text="根据OpenId群发消息" OnClick="OpenIdMessage_Click" />
    </div>
    </form>
</body>
</html>
