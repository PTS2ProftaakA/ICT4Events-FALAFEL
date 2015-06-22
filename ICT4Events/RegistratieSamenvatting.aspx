<%@ Page Title="" Language="C#" MasterPageFile="~/Registratie.Master" AutoEventWireup="true" CodeBehind="RegistratieSamenvatting.aspx.cs" Inherits="ICT4Events.RegistratieSamenvatting" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form class="form-horizontal col-lg-6 col-lg-offset-3" runat="server">
        <h2><br/>Samenvatting</h2>
        <fieldset>
            <div class="col-lg-6">
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
            </div>
            <div class="col-lg-6">
                <asp:Label Text="Producten" runat="server" AssociatedControlID="productLijst"/><br/>
                <asp:BulletedList runat="server" ID="productLijst"/><br/><br/>
                <asp:Label Text="Producten" runat="server" AssociatedControlID="Vrienden"/><br/>
                <asp:BulletedList runat="server" ID="Vrienden"/><br/><br/>
                <asp:Button runat="server" CssClass="btn btn-primary" ID="btnBevestig" Text="Bevestig" OnClick="btnBevestig_OnClick"/>
            </div>
        </fieldset>
    </form>
</asp:Content>