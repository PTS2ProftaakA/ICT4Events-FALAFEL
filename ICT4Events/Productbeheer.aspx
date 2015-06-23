<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Productbeheer.aspx.cs" Inherits="ICT4Events.Productbeheer" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form id="formProducten" runat="server">
        <div>
            <h1>Productbeheer</h1>
            <div class="col-lg-6">
                <div class="form-group input-group">
                    <asp:DropDownList ID="ddlProducten" CssClass="form-control" OnSelectedIndexChanged="ddlProducten_OnSelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
                    <span class="input-group-btn"><asp:Button ID="btnVerwijderen" CssClass="btn btn-primary" runat="server" Text="Verwijderen" CausesValidation="False" OnClick="btnVerwijderen_OnClick" /></span>
                </div>
                <br/>
                <asp:Label AssociatedControlID="ddlProductCategorien" runat="server" Text="Categorien"></asp:Label>
                <br/>
                <asp:DropDownList ID="ddlProductCategorien" CssClass="form-control" runat="server"></asp:DropDownList>
                <asp:RangeValidator runat="server" ControlToValidate="ddlProductCategorien" ErrorMessage="Kies een categorie." MinimumValue="1" MaximumValue="999"></asp:RangeValidator>
                <br/><br/>
                <asp:Label AssociatedControlID="tbMerk" runat="server" Text="Merk"></asp:Label>
                <br/>
                <asp:TextBox ID="tbMerk" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbMerk" ErrorMessage="Vul het merk van het product in."></asp:RequiredFieldValidator>
                <br/><br/>
                <asp:Label AssociatedControlID="tbSerie" runat="server" Text="Serie"></asp:Label>
                <br/>
                <asp:TextBox ID="tbSerie" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbSerie" ErrorMessage="Vul de serie van het product in."></asp:RequiredFieldValidator>
                <br/><br/>
                <asp:Label AssociatedControlID="tbTypeNummer" runat="server" Text="Type nummer"></asp:Label>
                <br/>
                <asp:TextBox ID="tbTypeNummer" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTypeNummer" ErrorMessage="Vul het typenummer van het product in."></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ControlToValidate="tbTypeNummer" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Vul een getal in bij het typenummer."></asp:CompareValidator>
                <br/><br/>
                <asp:Label AssociatedControlID="tbPrijs" runat="server" Text="Prijs"></asp:Label>
                <br/>
                <asp:TextBox ID="tbPrijs" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrijs" ErrorMessage="Vul de prijs van het product in."></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ControlToValidate="tbPrijs" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Vul een getal in bij de prijs."></asp:CompareValidator>
                <br/>
                <asp:Button ID="btnAanpassenMaken" CssClass="btn btn-primary" runat="server" Text="Toevoegen" OnClick="btnAanpassenMaken_OnClick"/>
            </div>
            <div class="col-lg-6">
                <div class="form-group input-group">
                    <asp:TextBox ID="tbAantalAanmaken" runat="server" min="0" max="100" CssClass="form-control contentWidth" step="1"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="btnExemplaarAanmaken" CssClass="btn btn-primary" runat="server" Text="Exemplaar(en) aanmaken" ValidationGroup="exemplaarAanmaken" OnClick="btnExemplaarAanmaken_OnClick"/>
                    </span>
                </div>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbAantalAanmaken" ValidationGroup="exemplaarAanmaken" ErrorMessage="Vul het aantal in dat je wilt aanmaken."></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ControlToValidate="tbAantalAanmaken" ValidationGroup="exemplaarAanmaken" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Vul een getal in."></asp:CompareValidator>
                <br/>
                <asp:Label AssociatedControlID="ddlProductExemplaren" runat="server" Text="Exemplaren"></asp:Label>
                <br/>
                <div class="input-group">
                    <asp:DropDownList ID="ddlProductExemplaren" CssClass="form-control" runat="server"></asp:DropDownList>
                    <span class="input-group-btn"><asp:Button ID="btnExemplaarVerwijderen" CssClass="btn btn-primary" runat="server" Text="Exemplaar verwijderen" ValidationGroup="exemplaarVerwijderen" OnClick="btnExemplaarVerwijderen_OnClick"/></span>
                </div>
                <asp:RangeValidator runat="server" ControlToValidate="ddlProductExemplaren" ErrorMessage="Kies een productexemplaar." ValidationGroup="exemplaarVerwijderen" MinimumValue="1" MaximumValue="999"></asp:RangeValidator>
                <br/><br/>
                <asp:Label AssociatedControlID="ddlVerhuringen" runat="server" Text="Verhuurde exemplaren"></asp:Label>
                <br/>
                <div class="input-group">
                    <asp:DropDownList ID="ddlVerhuringen" CssClass="form-control" runat="server"></asp:DropDownList>
                    <span class="input-group-btn"><asp:Button ID="btnVerhuringVerwijderen" CssClass="btn btn-primary" runat="server" Text="Verhuring verwijderen" ValidationGroup="verhuurVerwijderen" OnClick="btnVerhuringVerwijderen_OnClick"/></span>
                </div>
                <asp:RangeValidator runat="server" ControlToValidate="ddlVerhuringen" ErrorMessage="Kies een verhuring." ValidationGroup="verhuurVerwijderen" MinimumValue="1" MaximumValue="999"></asp:RangeValidator>
                <br/>
                <br/><br/>
                <asp:Panel runat="server" DefaultButton="btnInleveren" CssClass="input-group">
                    <asp:TextBox ID="tbBarCodeScanner" CssClass="form-control" runat="server"></asp:TextBox>
                    <span class="input-group-btn"><asp:Button ID="btnInleveren" CssClass="btn btn-primary" runat="server" Text="Inleveren" CausesValidation="False" OnClick="btnInleveren_OnClick"/></span>
                </asp:Panel>
            </div>
        </div>
    </form>
</asp:Content>