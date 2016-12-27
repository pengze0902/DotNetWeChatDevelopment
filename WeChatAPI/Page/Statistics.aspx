<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="WeChatAPI.Page.Statistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="UserAnalysis" runat="server" Text="用户分析数据统计" OnClick="UserAnalysis_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GraphicAnalysis" runat="server" Text="图文分析数据统计" OnClick="GraphicAnalysis_Click" />
        </div>
        <div></div>
        <div>
        <asp:Button ID="NewsAnalysis" runat="server" Text="消息分析数据统计" OnClick="NewsAnalysis_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="InterfaceAnalysis" runat="server" Text="接口分析数据统计" OnClick="InterfaceAnalysis_Click" />
    </div>
    </form>
</body>
</html>
