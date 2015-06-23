<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Eventbeheer.aspx.cs" Inherits="ICT4Events.Eventbeheer" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form id="formEvenementen" runat="server">
        <div>
            <h1>Event Beheer</h1>
            <div class="col-lg-6">
                <p>
                    <asp:Label AssociatedControlID="ddlEvenementen" runat="server"  AppendDataBoundItems="true" Text="Evenementen."></asp:Label>
                    <br/>
                    <div class="form-group input-group">
                        <asp:DropDownList ID="ddlEvenementen" CssClass="form-control" OnSelectedIndexChanged="ddlEvenementen_OnSelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
                        <span class="input-group-btn">
                            <asp:Button ID="btnVerwijderEvent" CssClass="btn btn-primary" runat="server" CausesValidation="False" OnClick="btnVerwijderEvent_OnClick" Text="Verwijder" />
                        </span>
                    </div>
                </p>
                <div class="form-group">
                    <p>
                        <asp:Label ID="lblEventName" runat="server" Text="Naam van het evenement."></asp:Label>
                        <asp:TextBox ID="tbEvenementNaam" CssClass="form-control" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbEvenementNaam" ErrorMessage="Vul de naam van het evenement in."></asp:RequiredFieldValidator>
                    </p>
                </div>
                <div class="form-group">
                    <p>
                        <asp:Label ID="lblStartDatum" runat="server" Text="Begindatum van het evenement."></asp:Label>
                        <asp:Calendar ID="calStartDatum" runat="server"></asp:Calendar>
                        <asp:CustomValidator runat="server" ID="calStartDatumValidator" OnServerValidate="calStartDatumValidator_OnServerValidate"></asp:CustomValidator>
                        <asp:Label ID="lblStartDatumValidatie" runat="server" Text=""></asp:Label>
                    </p>
                </div>
                <div class="form-group">
                    <p>
                        <asp:Label ID="lblEindDatum" runat="server" Text="Einddatum van het evenement."></asp:Label>
                        <asp:Calendar ID="calEindDatum" runat="server"></asp:Calendar>
                        <asp:CustomValidator runat="server" ID="calEindDatumValidator" OnServerValidate="calEindDatumValidator_OnServerValidate"></asp:CustomValidator>
                        <asp:Label ID="lblEindDatumValidatie" runat="server" Text=""></asp:Label>
                    </p>
                </div>
                <div class="form-group">
                    <p>
                        <asp:Label ID="lblMaxAantalBezoekers" runat="server" Text="Maximaal aantal bezoekers."></asp:Label>
                        <asp:TextBox ID="tbMaxAantalBezoekers" CssClass="form-control" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbMaxAantalBezoekers" ErrorMessage="Vul de hoeveelheid personen in."></asp:RequiredFieldValidator>
                    </p>
                </div>
                <div class="form-group">
                    <p>
                        <asp:Label AssociatedControlID="ddlLocaties" runat="server"  Text="Locaties."></asp:Label>
                        <br/>
                        <asp:DropDownList ID="ddlLocaties" CssClass="form-control" AutoPostBack="False" runat="server"></asp:DropDownList>
                        <asp:RangeValidator runat="server" ControlToValidate="ddlLocaties" ErrorMessage="Kies een locatie." MinimumValue="1" MaximumValue="999"></asp:RangeValidator>
                    </p>
                </div>
                <p>
                    <asp:Button ID="btnEvenementCreeren" CssClass="btn btn-primary" runat="server" OnClick="btnEvenementCreeren_Click" Text="Aanmaken" />
                </p>
                <asp:Label ID="lblErrorLabel" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-lg-6">
                <asp:Button ID="btnGebruikersOphalen" CssClass="btn btn-primary" runat="server" CausesValidation="False" OnClick="btnGebruikersOphalen_OnClick" Text="Gebruikers aanwezigheid ophalen" />
                <br/>
                <asp:Label AssociatedControlID="ddlAanwezigen" CssClass="form-control" runat="server" AppendDataBoundItems="true" Text="Aanwezigen."></asp:Label>
                <br/>
                <asp:DropDownList ID="ddlAanwezigen" CssClass="form-control" AutoPostBack="True" runat="server"></asp:DropDownList>
                <br/>
                <br/>
                <asp:Label AssociatedControlID="ddlAfwezigen" CssClass="form-control" runat="server" AppendDataBoundItems="true" Text="Afwezigen."></asp:Label>
                <br/>
                <asp:DropDownList ID="ddlAfwezigen" CssClass="form-control" AutoPostBack="True" runat="server"></asp:DropDownList>
                <br/>
                <br />
                <asp:Button ID="btnPlekkenOphalen" CssClass="btn btn-primary" runat="server" CausesValidation="False" OnClick="btnPlekkenOphalen_OnClick" Text="Beschikbaarheid plekken ophalen" />
                <br/> 
                <asp:Label AssociatedControlID="ddlVerkrijgbarePlekken" CssClass="form-control" runat="server" AppendDataBoundItems="true" Text="Verkrijgbare plekken."></asp:Label>
                <br/>
                <asp:DropDownList ID="ddlVerkrijgbarePlekken" CssClass="form-control" AutoPostBack="True" runat="server"></asp:DropDownList>
                <br/>
                <br/>
                <asp:Label AssociatedControlID="ddlVerkrijgbarePlekken" CssClass="form-control" runat="server" AppendDataBoundItems="true" Text="Bezette plekken."></asp:Label>
                <br/>
                <asp:DropDownList ID="ddlBezettePlekken" CssClass="form-control" AutoPostBack="True" runat="server"></asp:DropDownList>
            </div>
        </div>
    </form>
</asp:Content>