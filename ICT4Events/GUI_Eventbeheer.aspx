<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GUI_Eventbeheer.aspx.cs" Inherits="ICT4Events.GUI_Eventbeheer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Event beheer systeem</h1>
        <p>
            <asp:Label AssociatedControlID="ddlEvenementen" runat="server"  AppendDataBoundItems="true" Text="Evenementen."></asp:Label>
            <br/>
            <asp:DropDownList ID="ddlEvenementen" OnSelectedIndexChanged="ddlEvenementen_OnSelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
            <asp:Button ID="btnVerwijderEvent" runat="server" CausesValidation="False" OnClick="btnVerwijderEvent_OnClick" Text="Verwijder" />
        </p>
        <p>
            <asp:Label ID="lblEventName" runat="server" Text="Naam van het evenement."></asp:Label>
        </p>
        <p>
            <asp:TextBox ID="tbEvenementNaam" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbEvenementNaam" ErrorMessage="Vul de naam van het evenement in."></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblStartDatum" runat="server" Text="Begindatum van het evenement."></asp:Label>
            <asp:Calendar ID="calStartDatum" runat="server"></asp:Calendar>
            <asp:CustomValidator runat="server" ID="calStartDatumValidator" OnServerValidate="calStartDatumValidator_OnServerValidate"></asp:CustomValidator>
            <asp:Label ID="lblStartDatumValidatie" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblEindDatum" runat="server" Text="Einddatum van het evenement."></asp:Label>
        </p>
        <asp:Calendar ID="calEindDatum" runat="server"></asp:Calendar>
        <asp:CustomValidator runat="server" ID="calEindDatumValidator" OnServerValidate="calEindDatumValidator_OnServerValidate"></asp:CustomValidator>
        <asp:Label ID="lblEindDatumValidatie" runat="server" Text=""></asp:Label>
        <p>
            <asp:Label ID="lblMaxAantalBezoekers" runat="server" Text="Maximaal aantal bezoekers."></asp:Label>
        </p>
        <p>
            <asp:TextBox ID="tbMaxAantalBezoekers" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbMaxAantalBezoekers" ErrorMessage="Vul de hoeveelheid personen in."></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label AssociatedControlID="ddlLocaties" runat="server"  Text="Locaties."></asp:Label>
            <br/>
            <asp:DropDownList ID="ddlLocaties" AutoPostBack="False" runat="server"></asp:DropDownList>
            <asp:RangeValidator runat="server" ControlToValidate="ddlLocaties" ErrorMessage="Kies een locatie." MinimumValue="1" MaximumValue="999"></asp:RangeValidator>
        </p>
        <p>
            <asp:Button ID="btnEvenementCreeren" runat="server" OnClick="btnEvenementCreeren_Click" Text="Aanmaken" />
        </p
        <asp:Label ID="lblErrorLabel" runat="server" Text=""></asp:Label>

        <br />
        <asp:Button ID="btnGebruikersOphalen" runat="server" CausesValidation="False" OnClick="btnGebruikersOphalen_OnClick" Text="Gebruikers aanwezigheid ophalen" />
        <br/>
        <asp:Label AssociatedControlID="ddlAanwezigen" runat="server" AppendDataBoundItems="true" Text="Aanwezigen."></asp:Label>
        <br/>
        <asp:DropDownList ID="ddlAanwezigen" AutoPostBack="True" runat="server"></asp:DropDownList>
        <br/>
        <br/>
        <asp:Label AssociatedControlID="ddlAfwezigen" runat="server" AppendDataBoundItems="true" Text="Afwezigen."></asp:Label>
        <br/>
        <asp:DropDownList ID="ddlAfwezigen" AutoPostBack="True" runat="server"></asp:DropDownList>
        <br/>
        <br />
        <asp:Button ID="btnPlekkenOphalen" runat="server" CausesValidation="False" OnClick="btnPlekkenOphalen_OnClick" Text="Beschikbaarheid plekken ophalen" />
        <br/> 
        <asp:Label AssociatedControlID="ddlVerkrijgbarePlekken" runat="server" AppendDataBoundItems="true" Text="Verkrijgbare plekken."></asp:Label>
        <br/>
        <asp:DropDownList ID="ddlVerkrijgbarePlekken" AutoPostBack="True" runat="server"></asp:DropDownList>
        <br/>
        <br/>
        <asp:Label AssociatedControlID="ddlVerkrijgbarePlekken" runat="server" AppendDataBoundItems="true" Text="Bezette plekken."></asp:Label>
        <br/>
        <asp:DropDownList ID="ddlBezettePlekken" AutoPostBack="True" runat="server"></asp:DropDownList>
    </form>
</body>
</html>
