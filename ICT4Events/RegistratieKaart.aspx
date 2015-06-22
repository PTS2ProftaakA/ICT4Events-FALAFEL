<%@ Page Title="" Language="C#" MasterPageFile="~/Registratie.Master" AutoEventWireup="true" CodeBehind="RegistratieKaart.aspx.cs" Inherits="ICT4Events.RegistratieKaart" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form class="form-horizontal col-lg-6 col-lg-offset-3" runat="server">
        <h2><br/></h2>
        <fieldset>
            <div class="col-lg-3">
                <asp:DropDownList CssClass="form-control" ID="dropPlaatsen" runat="server" AutoPostBack="True" />
                <br/><br/>
                <asp:panel runat="server" ScrollBars="None">
                    <asp:Label runat="server">Maximale hoeveelheid personen</asp:Label>
                    <br/>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtHoeveelheidPersonen" Enabled="False">6</asp:TextBox>
                    <br/><br/>
                    <asp:Label runat="server">Specificaties</asp:Label>
                    <br/>
                    <asp:panel runat="server" CssClass="form-control" ScrollBars="Vertical" Height="200px" ID="pnlSpecificaties" />
                </asp:panel>
                <br/>
                <asp:Panel runat="server" ScrollBars="None">
                    <asp:Label runat="server">Uw gekozen plaatsen</asp:Label>
                    <br/>
                    <asp:panel runat="server" CssClass="form-control " ScrollBars="Vertical" Height="100px">
                        <asp:CheckBoxList ID="dropTeHurenPlaatsen" runat="server" />
                    </asp:Panel>
                </asp:Panel>
                <br/>
                <asp:Button runat="server" CssClass="btn btn-primary" ID="btnVolgende" Text="Volgende" OnClick="btnVolgende_OnClick"/>
                <br/><br/>
                <asp:Label runat="server" ID="lblError">Je moet minstens één plaats kiezen</asp:Label>
            </div>
            <div class="col-lg-8 col-lg-offset-1">
                <asp:Image runat="server" ImageUrl="~/Resources/CampingKaart.png" Width="100%"/>
            </div>
        </fieldset>
    </form>
</asp:Content>
