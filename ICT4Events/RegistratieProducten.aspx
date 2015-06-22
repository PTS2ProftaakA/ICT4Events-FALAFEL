<%@ Page Title="" Language="C#" MasterPageFile="~/Registratie.Master" AutoEventWireup="true" CodeBehind="RegistratieProducten.aspx.cs" Inherits="ICT4Events.RegistratieProducten" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form class="form-horizontal col-lg-6 col-lg-offset-3" runat="server">
        <h2><br/></h2>
        <fieldset>
            <div class="col-lg-6">
                <div class="input-group">
                    <asp:TextBox runat="server" placeholder="Zoeken" CssClass="form-control" ID="txtZoek" OnTextChanged="txtZoek_OnTextChanged"/>
                    <span class="input-group-btn"><asp:Button Text="Zoek" CssClass="btn btn-flat btn-primary" runat="server" ID="btnZoek"/></span>
                </div><br/><br/>
                <asp:Panel runat="server" ID="pnlProducten" Width="500px"/><br/>
            </div>
            <div class="col-lg-5 col-lg-offset-1">
                <asp:Panel runat="server" ID="pnlWinkelwagen" Width="500px"/><br/>
                <asp:Label runat="server" ID="txtHuurDatum"/><br/><br/>
                <asp:Button runat="server" CssClass="btn btn-primary" ID="btnVerder" Text="Verder" OnClick="btnVerder_OnClick"/>
            </div>
        </fieldset>
    </form>
</asp:Content>