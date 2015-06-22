<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GUI_Productbeheer.aspx.cs" Inherits="ICT4Events.GUI_Productbeheer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Productbeheer</h1>
        <asp:Label ID="lblProducten" runat="server" Text="Producten"></asp:Label>
        <br/>
        <asp:DropDownList ID="ddlProducten" OnSelectedIndexChanged="ddlProducten_OnSelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
        <br/>
        <asp:Button ID="btnVerwijderen" runat="server" Text="Verwijderen" CausesValidation="False" OnClick="btnVerwijderen_OnClick" /><br/>
        <br/>
        <asp:Label AssociatedControlID="ddlProductCategorien" runat="server" Text="Categorien"></asp:Label>
        <br/>
        <asp:DropDownList ID="ddlProductCategorien" runat="server"></asp:DropDownList>
        <asp:RangeValidator runat="server" ControlToValidate="ddlProductCategorien" ErrorMessage="Kies een categorie." MinimumValue="1" MaximumValue="999"></asp:RangeValidator>
        <br/>
        <br/>
        <asp:Label AssociatedControlID="tbMerk" runat="server" Text="Merk"></asp:Label>
        <br/>
        <asp:TextBox ID="tbMerk" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbMerk" ErrorMessage="Vul het merk van het product in."></asp:RequiredFieldValidator>
        <br/>
        <br/>
        <asp:Label AssociatedControlID="tbSerie" runat="server" Text="Serie"></asp:Label>
        <br/>
        <asp:TextBox ID="tbSerie" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbSerie" ErrorMessage="Vul de serie van het product in."></asp:RequiredFieldValidator>
        <br/>
        <br/>
        <asp:Label AssociatedControlID="tbTypeNummer" runat="server" Text="Type nummer"></asp:Label>
        <br/>
        <asp:TextBox ID="tbTypeNummer" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTypeNummer" ErrorMessage="Vul het typenummer van het product in."></asp:RequiredFieldValidator>
        <asp:CompareValidator runat="server" ControlToValidate="tbTypeNummer" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Vul een getal in bij het typenummer."></asp:CompareValidator>
        <br/>
        <br/>
        <asp:Label AssociatedControlID="tbPrijs" runat="server" Text="Prijs"></asp:Label>
        <br/>
        <asp:TextBox ID="tbPrijs" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrijs" ErrorMessage="Vul de prijs van het product in."></asp:RequiredFieldValidator>
        <asp:CompareValidator runat="server" ControlToValidate="tbPrijs" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Vul een getal in bij de prijs."></asp:CompareValidator>
        <br/>
        <asp:Button ID="btnAanpassenMaken" runat="server" Text="Toevoegen" OnClick="btnAanpassenMaken_OnClick"/>
        <br/>
        <br/>
        <asp:TextBox ID="tbAantalAanmaken" runat="server" min="0" max="100" CssClass = "contentWidth" step="1"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbAantalAanmaken" ValidationGroup="exemplaarAanmaken" ErrorMessage="Vul het aantal in dat je wilt aanmaken."></asp:RequiredFieldValidator>
        <asp:CompareValidator runat="server" ControlToValidate="tbAantalAanmaken" ValidationGroup="exemplaarAanmaken" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Vul een getal in."></asp:CompareValidator>
        <br/>
        <asp:Button ID="btnExemplaarAanmaken" runat="server" Text="Exemplaar(en) aanmaken" ValidationGroup="exemplaarAanmaken" OnClick="btnExemplaarAanmaken_OnClick"/>
        <br/>
        <br/>
        <asp:Label AssociatedControlID="ddlProductExemplaren" runat="server" Text="Exemplaren"></asp:Label>
        <br/>
        <asp:DropDownList ID="ddlProductExemplaren" runat="server"></asp:DropDownList>
        <asp:RangeValidator runat="server" ControlToValidate="ddlProductExemplaren" ErrorMessage="Kies een productexemplaar." ValidationGroup="exemplaarVerwijderen" MinimumValue="1" MaximumValue="999"></asp:RangeValidator>
        <br/>
        <asp:Button ID="btnExemplaarVerwijderen" runat="server" Text="Exemplaar verwijderen" ValidationGroup="exemplaarVerwijderen" OnClick="btnExemplaarVerwijderen_OnClick"/>
        <br/>
        <br/>
        <asp:Label AssociatedControlID="ddlVerhuringen" runat="server" Text="Verhuurde exemplaren"></asp:Label>
        <br/>
        <asp:DropDownList ID="ddlVerhuringen" runat="server"></asp:DropDownList>
        <asp:RangeValidator runat="server" ControlToValidate="ddlVerhuringen" ErrorMessage="Kies een verhuring." ValidationGroup="verhuurVerwijderen" MinimumValue="1" MaximumValue="999"></asp:RangeValidator>
        <br/>
        <asp:Button ID="btnVerhuringVerwijderen" runat="server" Text="Verhuring verwijderen" ValidationGroup="verhuurVerwijderen" OnClick="btnVerhuringVerwijderen_OnClick"/>
        <br/>
        <br/>
        <asp:Panel runat="server" DefaultButton="btnInleveren">
            <asp:TextBox ID="tbBarCodeScanner" runat="server"></asp:TextBox>
            <br/>
            <asp:Button ID="btnInleveren" runat="server" Text="Inleveren" CausesValidation="False" OnClick="btnInleveren_OnClick"/>
        </asp:Panel>
        <br/>
        <br/>
    </div>
    </form>
</body>
</html>
