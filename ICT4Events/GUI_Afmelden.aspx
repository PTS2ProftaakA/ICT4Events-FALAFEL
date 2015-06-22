<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GUI_Afmelden.aspx.cs" Inherits="ICT4Events.GUI_Afmelden" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form ID="form1" runat="server">
        <title>ICT4Events - Afmelden</title>
        <h1>Afmelden</h1>
        <asp:Label ID="lblWaarschuwing" runat="server" Text="Weet je zeker dat je jezelf wilt afmelden voor het evenement?"></asp:Label>
        <br/>
        <br/>
        <asp:Button ID="btnVerwijderAccount" runat="server" OnClick="btnVerwijderAccount_OnClick" Text="Afmelden" />
    </form>
</asp:Content>

