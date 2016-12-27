<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QrCode.aspx.cs" Inherits="WeChatAPI.Page.QrCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="QrCodeCreate" runat="server" Text="创建二维码" OnClick="QrCodeCreate_Click" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ObtainQrCode" runat="server" Text="获取二维码" OnClick="ObtainQrCode_Click" />
        </div>
    </form>
</body>
</html>
