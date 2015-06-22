<%@ Page Title="" Language="C#" MasterPageFile="~/Registratie.Master" AutoEventWireup="true" CodeBehind="RegistratieVrienden.aspx.cs" Inherits="ICT4Events.RegistratieVrienden" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form class="form-horizontal col-lg-6 col-lg-offset-3" runat="server">
        <h2><br/>Vrienden Opgeven</h2>
        <fieldset>
            <asp:Panel runat="server" ID="pnlGegevens"/>
            <asp:Label runat="server" ID="lblError"/><br/>
            <asp:Button runat="server" CssClass="btn btn-primary" Text="Verder" ID="btnVerder" OnClick="btnVerder_OnClick"/>
        </fieldset>
    </form>
</asp:Content>