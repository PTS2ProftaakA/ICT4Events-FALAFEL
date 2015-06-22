<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediaSharingOmgeving.aspx.cs" Inherits="ICT4Events.MediaSharingOmgeving" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!-- TODO  0De hoogte gelijk maken aan de hoogte van het bestand + de knoppen eronder, en de breedtje 1 waarde -->
        <asp:Label ID="lbSorteerCat" runat="server" Text="Sorteer op Categorie:"></asp:Label>
        <asp:DropDownList ID="ddlSearch" runat="server" OnSelectedIndexChanged="ddlSearch_OnSelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Text="Alles" Value="AlleCategorie"></asp:ListItem>
        </asp:DropDownList><br/><br/>
        <asp:Label ID="lblSorteerExt" runat="server" Text="Sorteer op Extentie:"></asp:Label>
        <asp:DropDownList ID="ddlExtensionSearch" runat="server" OnSelectedIndexChanged="ddlExtensionSearch_OnSelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Text="Alles" Value="AlleExtensies"></asp:ListItem>
        </asp:DropDownList><br/><br/>
            <asp:PlaceHolder ID="phBestand" runat="server"></asp:PlaceHolder>
            <br />
            <br /> 
         <br />
        <asp:TreeView ID="tvCategorie" runat="server" HoverNodeStyle-ForeColor="Red" OnSelectedNodeChanged="tvCategorie_OnSelectedNodeChanged">

        </asp:TreeView> <br/><br/>
        <asp:Label ID="lbCategorieNaam" runat="server" Text="Categorie naam:"></asp:Label><br/>
        <asp:TextBox ID="txtCategorie" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:DropDownList ID="ddlCategorie" runat="server">
            <asp:ListItem Text="Main" Value="MainCategory"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="ValidatortxtCategorie" ControlToValidate="txtCategorie" runat="server" ErrorMessage="Veld is verplicht" ValidationGroup="nieuweCategorie"></asp:RequiredFieldValidator>
        <br/>
        <asp:Button ID="btnMaakCategorie" onClick="btnMaakCategorie_OnClick" runat="server" Text="Maak Categorie" ValidationGroup="nieuweCategorie"/>
        <asp:Button ID="btnVerwijderCategorie" OnClick="btnVerwijderCategorie_OnClick" runat="server" Text="Verwijder Categorie" /> <br/><br/>
        <input type="file" id="fileInput" runat="server"/> <br/>
        <input type="submit" id="fileSubmit" value="Upload" runat="server"/>
        
        
        <asp:PlaceHolder ID="phCategorieReacties" runat="server">
            <asp:Panel ID="pnlGehelePost" BorderColor="#000000" BorderWidth="1" Width="640" Height="100%" BorderStyle="Solid" runat="server">
                <asp:Label ID="lbCatNaam" runat="server" Text="Categorie naam here"></asp:Label> <br/>
                <asp:Button ID="btnLike" OnClick="btnLikeCategorie_OnClick" runat="server" Text="Like" />
                <asp:Label ID="lbCounter" runat="server" Text="Aantal Likes"></asp:Label>
                <asp:Button ID="btnRaporteren" OnClick="btnRaporterenCategorie_OnClick" runat="server" Text="Rapporteer" />
                <br/><br/>
                <asp:Label ID="lbTitel" runat="server" Text="Titel" AssociatedControlID="txtTitel"></asp:Label> <br/>
                <asp:TextBox ID="txtTitel" runat="server" Width="600" MaxLength="255" AutoCompleteType="Disabled"></asp:TextBox> <br /> <br/>
                <asp:Label ID="lbInhoud" runat="server" Text="Inhoud" AssociatedControlID="txtInhoud"></asp:Label> <br/>
                <asp:TextBox ID="txtInhoud" runat="server" Rows="3" TextMode="MultiLine" Style="resize: none" Width="600" MaxLength="255" AutoCompleteType="Disabled"></asp:TextBox> <br/>
                <asp:RequiredFieldValidator ID="ValidatorInhoud" ControlToValidate="txtInhoud" runat="server" ErrorMessage="U moet een bericht invullen" ValidationGroup="vgInhoud"></asp:RequiredFieldValidator>
                <br/>
                <asp:Button ID="btnReageer" runat="server" Text="Plaats reactie" OnClick="btnReageerCategorie_OnClick" ValidationGroup="vgInhoud"/> <br/><br/>
                <br/><br/>
                <asp:PlaceHolder ID="phBerichten" runat="server"></asp:PlaceHolder>
            </asp:Panel>    
        </asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
