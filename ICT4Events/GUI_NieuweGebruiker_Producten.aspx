<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GUI_NieuweGebruiker_Producten.aspx.cs" Inherits="ICT4Events.GUI_NieuweGebruiker_Producten" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txtZoek" OnTextChanged="txtZoek_OnTextChanged"/>
        <asp:Button Text="Zoek" runat="server" ID="btnZoek"/><br/>
        <asp:Literal Text="=====================================================================================" runat="server" /><br/>
        <asp:Panel runat="server" ID="pnlProducten" Width="500px">
            
        </asp:Panel>
        <asp:Literal Text="=====================================================================================" runat="server" /><br/>
        <asp:Panel runat="server" ID="pnlWinkelwagen" Width="500px">
            
        </asp:Panel><br/>
        <asp:Label runat="server" ID="txtHuurDatum"/><br/>
        <br/>
        <asp:Button runat="server" ID="btnVerder" Text="Verder" OnClick="btnVerder_OnClick"/>
    </div>
    </form>
</body>
</html>
