<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReactiePost.aspx.cs" Inherits="ICT4Events.ReactiePost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlGehelePost" BorderColor="#000000" BorderWidth="1" Width="640" Height="100%" BorderStyle="Solid" runat="server">
            <asp:Label ID="lbUsername" runat="server" Text="username poster here"></asp:Label> <br/>
            <asp:PlaceHolder ID="phReactie" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="phVerwijder" runat="server">
                
            </asp:PlaceHolder> 
            <asp:Button ID="btnLike" OnClick="btnLike_OnClick" runat="server" Text="Like" />
        <asp:Label ID="lbCounter" runat="server" Text="Aantal Likes"></asp:Label>
        <asp:Button ID="btnRaporteren" OnClick="btnRaporteren_OnClick" runat="server" Text="Button" />
        <br/><br/>
        <asp:Label ID="lbTitel" runat="server" Text="Titel" AssociatedControlID="txtTitel"></asp:Label> <br/>
        <asp:TextBox ID="txtTitel" runat="server" Width="600" MaxLength="255" AutoCompleteType="Disabled"></asp:TextBox> <br /> <br/>
        <asp:Label ID="lbInhoud" runat="server" Text="Inhoud" AssociatedControlID="txtInhoud"></asp:Label> <br/>
        <asp:TextBox ID="txtInhoud" runat="server" Rows="3" TextMode="MultiLine" Style="resize: none" Width="600" MaxLength="255" AutoCompleteType="Disabled"></asp:TextBox> <br/>
        <asp:RequiredFieldValidator ID="ValidatorInhoud" ControlToValidate="txtInhoud" runat="server" ErrorMessage="U moet een bericht invullen" ValidationGroup="vgInhoud"></asp:RequiredFieldValidator>
        <br/>
        <asp:Button ID="btnReageer" runat="server" Text="Plaats reactie" OnClick="btnReageer_OnClick" ValidationGroup="vgInhoud"/> <br/><br/>
        <asp:PlaceHolder ID="phBerichten" runat="server"></asp:PlaceHolder>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
