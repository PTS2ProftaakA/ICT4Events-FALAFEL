<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MediaSharing.aspx.cs" Inherits="ICT4Events.MediaSharing" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form id="formMedia" method="post" enctype="multipart/form-data" runat="server">
        <div>
            <div class="col-lg-6">
                <asp:Label ID="lbSorteerCat" runat="server" Text="Sorteer op Categorie:" />
                <asp:DropDownList ID="ddlSearch" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlSearch_OnSelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Text="Alles" Value="AlleCategorie" />
                </asp:DropDownList>
                <br/><br/>
                <asp:Label ID="lblSorteerExt" runat="server" Text="Sorteer op Extentie:" />
                <asp:DropDownList ID="ddlExtensionSearch" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlExtensionSearch_OnSelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Text="Alles" Value="AlleExtensies" />
                </asp:DropDownList>
                <br/><br/>
                <asp:PlaceHolder ID="phBestand" runat="server" />
            </div>
            <div class="col-lg-6">
                <asp:TreeView ID="tvCategorie" runat="server" HoverNodeStyle-ForeColor="Red" OnSelectedNodeChanged="tvCategorie_OnSelectedNodeChanged">
                    <SelectedNodeStyle Font-Bold="True" />
                </asp:TreeView>
                <br/><br/><br/>
                <div class="form-group">
                    <asp:Label ID="lbCategorieNaam" runat="server" Text="Categorie naam:" /><br/>
                    <asp:TextBox ID="txtCategorie" CssClass="form-control" runat="server" AutoCompleteType="Disabled" />
                    <asp:DropDownList ID="ddlCategorie" CssClass="form-control" runat="server">
                        <asp:ListItem Text="Main" Value="MainCategory" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ValidatortxtCategorie" ControlToValidate="txtCategorie" runat="server" ErrorMessage="Veld is verplicht" ValidationGroup="nieuweCategorie" /><br/>
                    <asp:Button ID="btnMaakCategorie" CssClass="btn btn-primary" onClick="btnMaakCategorie_OnClick" runat="server" Text="Maak Categorie" ValidationGroup="nieuweCategorie"/>
                    <asp:Button ID="btnVerwijderCategorie" CssClass="btn btn-flat btn-primary" OnClick="btnVerwijderCategorie_OnClick" runat="server" Text="Verwijder Categorie" />
                </div>
                <br/><br/><br/>
                <div class="input-group">
                    <input type="file" class="form-control" id="fileInput" runat="server"/>
                    <span class="input-group-btn">
                        <input type="submit" class="btn btn-primary" id="fileSubmit" value="Upload" runat="server"/>
                    </span>
                </div>
                <br/><br/><br/>
                <asp:PlaceHolder ID="phCategorieReacties" runat="server">
                    <asp:Panel ID="pnlGehelePost" CssClass="well" Width="100%" Height="100%" runat="server">
                        <asp:Label ID="lbCatNaam" runat="server" Text="Categorie naam here" /><br/>
                        <asp:Button ID="btnLike" OnClick="btnLikeCategorie_OnClick" runat="server" Text="Like" />
                        <asp:Label ID="lbCounter" runat="server" Text="Aantal Likes"/>
                        <asp:Button ID="btnRaporteren" OnClick="btnRaporterenCategorie_OnClick" runat="server" Text="Rapporteer" />
                        <br/><br/>
                        <asp:Label ID="lbTitel" runat="server" Text="Titel" AssociatedControlID="txtTitel" /><br/>
                        <asp:TextBox ID="txtTitel" runat="server" Width="100%" MaxLength="255" AutoCompleteType="Disabled" />
                        <br/><br/>
                        <asp:Label ID="lbInhoud" runat="server" Text="Inhoud" AssociatedControlID="txtInhoud" /><br/>
                        <asp:TextBox ID="txtInhoud" runat="server" Rows="3" TextMode="MultiLine" Style="resize: none" Width="100%" MaxLength="255" AutoCompleteType="Disabled" /><br/>
                        <asp:RequiredFieldValidator ID="ValidatorInhoud" ControlToValidate="txtInhoud" runat="server" ErrorMessage="U moet een bericht invullen" ValidationGroup="vgInhoud" /><br/>
                        <asp:Button ID="btnReageer" runat="server" Text="Plaats reactie" OnClick="btnReageerCategorie_OnClick" ValidationGroup="vgInhoud"/>
                        <br/><br/><br/><br/>
                        <asp:PlaceHolder ID="phBerichten" runat="server"></asp:PlaceHolder>
                    </asp:Panel>    
                </asp:PlaceHolder>
            </div>
        </div>
    </form>
</asp:Content>