<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GUI_NieuweGebruiker_Vrienden.aspx.cs" Inherits="ICT4Events.GUI_NieuweGebruiker_Vrienden" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel runat="server" ID="pnlGegevens">
            
        </asp:Panel>
        <asp:Label runat="server" ID="lblError"/><br/>
        <asp:Button runat="server" Text="Verder" ID="btnVerder" OnClick="btnVerder_OnClick"/>
    </div>
    </form>
</body>
</html>
