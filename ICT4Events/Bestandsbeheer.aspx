<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Bestandsbeheer.aspx.cs" Inherits="ICT4Events.Bestandsbeheer" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form id="formBestanden" method="post" enctype="multipart/form-data" runat="server">
    <div>
        <asp:TreeView ID="fileTree" CollapseImageToolTip="Inklappen" ExpandImageToolTip="Uitvouwen" ShowLines="True" runat="server">
            <NodeStyle ForeColor="#000000" Font-Names="sans-serif" />
            <SelectedNodeStyle ForeColor="#ff0000" Font-Names="sans-serif" />
        </asp:TreeView>
        <br/><br/>
        <input type="submit" id="dlSubmit" value="Download" runat="server" />
        <br/><br/>
        <input type="file" id="fileInput" runat="server" />
        <input type="submit" id="fileSubmit" value="Upload" runat="server" />
        <br/><br/>
        <input type="text" id="dirInput" placeholder="Mapnaam" runat="server" />
        <input type="submit" id="dirSubmit" value="Maak map" runat="server" />
    </div>
    </form>
</asp:Content>