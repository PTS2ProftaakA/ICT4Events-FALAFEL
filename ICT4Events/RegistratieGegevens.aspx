<%@ Page Title="" Language="C#" MasterPageFile="~/Registratie.Master" AutoEventWireup="true" CodeBehind="RegistratieGegevens.aspx.cs" Inherits="ICT4Events.RegistratieGegevens" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentGoesHere" runat="server">
    <form class="form-horizontal col-lg-4 col-lg-offset-4" runat="server">
        <h2><br/>Gegevens</h2>
        <fieldset>
            <div class="form-group col-lg-12">
                <input type="text" class="form-control" id="txtVoorNaam" placeholder="Voornaam" runat="server" />
                <label for="txtVoorNaam" class="control-label">
                    <asp:RequiredFieldValidator ControlToValidate="txtVoorNaam" ErrorMessage="Vul een voornaam in a.u.b." runat="server" />
                </label>
            </div>
            <div class="form-group col-lg-12">
                <input type="text" class="form-control" id="txtTussenvoegsel" placeholder="Tussenvoegsel" runat="server" />
                <br/>
            </div>
            <div class="form-group col-lg-12">
                <input type="text" class="form-control" id="txtAchterNaam" placeholder="Achternaam" runat="server" />
                <label for="txtAchterNaam" class="control-label">
                    <asp:RequiredFieldValidator ControlToValidate="txtAchterNaam" ErrorMessage="Vul een achternaam in a.u.b." runat="server" />
                </label>
            </div>
            <div class="form-group col-lg-12">
                <div class="col-lg-6" style="padding-left: 0;">
                    <input type="text" class="form-control" id="txtStraatNaam" placeholder="Straatnaam" runat="server" />
                    <label for="txtStraatNaam" class="control-label">
                        <asp:RequiredFieldValidator ControlToValidate="txtStraatNaam" ErrorMessage="Vul een straatnaam in a.u.b." runat="server" />
                    </label>
                </div>
                <div class="col-lg-6" style="padding-right: 0;">
                    <input type="text" class="form-control" id="txtHuisnummer" placeholder="Nummer" runat="server" />
                    <label for="txtHuisnummer" class="control-label">
                        <asp:RequiredFieldValidator ControlToValidate="txtHuisnummer" ErrorMessage="Vul een huisnummer in a.u.b." runat="server" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtHuisnummer" ErrorMessage="De ingevoerde waarde is onjuist" ValidationExpression="[0-9]+[0-9-/]?[a-zA-Z]?" />
                    </label>
                </div>
            </div>
            <div class="form-group col-lg-12">
                <input type="text" class="form-control" id="txtWoonplaats" placeholder="Woonplaats" runat="server" />
                <label for="txtWoonplaats" class="control-label">
                    <asp:RequiredFieldValidator ControlToValidate="txtWoonplaats" ErrorMessage="Vul een woonplaats in a.u.b." runat="server" />
                </label>
            </div>
            <div class="form-group col-lg-12">
                <input type="text" class="form-control" id="txtBanknummer" placeholder="Banknummer (IBAN zonder spaties)" runat="server" />
                <label for="txtBanknummer" class="control-label">
                    <asp:RequiredFieldValidator ControlToValidate="txtBanknummer" ErrorMessage="Vul een banknummer in a.u.b." runat="server" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtBanknummer" ErrorMessage="De invoer voldoet niet aan de IBAN eisen" ValidationExpression="[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}" />
                </label>
            </div>
            <div class="form-group col-lg-12">
                <input type="text" class="form-control" id="txtEmail" placeholder="E-Mail" runat="server" />
                <label for="txtEmail" class="control-label">
                    <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="Vul een E-mail adres in a.u.b." runat="server" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Vul een geldig email adres in" ValidationExpression="[a-zA-Z0-9._%-]+@[a-zA-Z0-9._%-]+\.[a-zA-Z]{2,10}" />
                </label>
            </div>
            <div class="form-group col-lg-12">
                <input type="submit" class="btn btn-primary" style="float: right;" id="btnNext" runat="server" value="Volgende" />
            </div>
        </fieldset>
    </form>
</asp:Content>