<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GUI_NieuweGebruiker_Samenvatting.aspx.cs" Inherits="ICT4Events.GUI_NieuweGebruiker_Samenvatting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Persoon</h1>
        <asp:Label Text="Naam" runat="server" AssociatedControlID="lblPersoonNaam"/><br/>
        <asp:Label ID="lblPersoonNaam" runat="server" /><br/><br/>
        
        <asp:Label Text="Adres" runat="server" AssociatedControlID="lblAdres"/><br/>
        <asp:Label ID="lblAdres" runat="server" /><br/><br/>
        
        <asp:Label Text="Woonplaats" runat="server" AssociatedControlID="lblWoonplaats"/><br/>
        <asp:Label ID="lblWoonplaats" runat="server" /><br/><br/>
        
        <asp:Label Text="Banknummer" runat="server" AssociatedControlID="lblBanknummer"/><br/>
        <asp:Label ID="lblBanknummer" runat="server" /><br/><br/>
        
        <asp:Label Text="Plaatsen" runat="server" AssociatedControlID="plaatsLijst"/><br/>
        <asp:BulletedList runat="server" ID="plaatsLijst"/><br/><br/>
        
        <asp:Label Text="Producten" runat="server" AssociatedControlID="productLijst"/><br/>
        <asp:BulletedList runat="server" ID="productLijst"/><br/><br/>
        
        <asp:Label Text="Producten" runat="server" AssociatedControlID="Vrienden"/><br/>
        <asp:BulletedList runat="server" ID="Vrienden"/><br/>
        
        <br/>
        <asp:Button runat="server" ID="btnBevestig" Text="Bevestig" OnClick="btnBevestig_OnClick"/>
    </div>
    </form>
</body>
</html>
