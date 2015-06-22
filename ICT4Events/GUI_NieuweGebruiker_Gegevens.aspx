<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GUI_NieuweGebruiker_Gegevens.aspx.cs" Inherits="ICT4Events.GUI_NieuweGebruiker_Gegevens" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        td {
            height: 23px;
            position: relative;
        }
        td span {
            line-height: 23px;
            position: absolute;
            top: 0;
            left: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Gegevens:</h1>
        <table width ="1000px">
            <tr>
                <td>Voornaam :</td>
                <td colspan="3"><asp:TextBox runat="server" Width="100%" ID="txtVoorNaam"></asp:TextBox></td>   
            </tr>
            <tr>
                <td></td>
                <td colspan="3"><asp:RequiredFieldValidator runat="server" ControlToValidate="txtVoorNaam" ErrorMessage="Vul een voornaam in a.u.b."></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Tussenvoegsel :</td>
                <td colspan="3"><asp:TextBox runat="server" Width="100%" ID="txtTussenvoegsel"></asp:TextBox></td>
            </tr>
            <tr>
                 <td colspan="3"><p></p></td>
            </tr>
            <tr>
                <td>Achternaam :</td>
                <td colspan="3"><asp:TextBox runat="server" Width="100%" ID="txtAchterNaam"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3"><asp:RequiredFieldValidator runat="server" ControlToValidate="txtAchterNaam" ErrorMessage="Vul een achternaam in a.u.b."></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Straatnaam :</td>
                <td><asp:TextBox runat="server" Width="100%" ID="txtStraatNaam"></asp:TextBox></td>
                <td> Nummer : </td>
                <td><asp:TextBox runat="server" Width="100%" ID="txtHuisnummer"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RequiredFieldValidator runat="server" ControlToValidate="txtStraatNaam" ErrorMessage="Vul een straatnaam in a.u.b."></asp:RequiredFieldValidator></td>
                <td></td>
                <td>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHuisnummer" ErrorMessage="Vul een huisnummer in a.u.b." ID="errorHuisnummerGeenWaarde"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtHuisnummer" ErrorMessage="De ingevoerde waarde is onjuist" ValidationExpression="[0-9]+[0-9-/]?[a-zA-Z]?"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Woonplaats :</td>
                <td colspan="3"><asp:TextBox runat="server" Width="100%" ID="txtWoonplaats"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3"><asp:RequiredFieldValidator runat="server" ControlToValidate="txtWoonplaats" ErrorMessage="Vul een woonplaats in a.u.b."></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Banknummer:</td>
                <td colspan="3"><asp:TextBox runat="server" Width="100%" ID="txtBanknummer"></asp:TextBox></td>
            </tr>
            <tr>
                <td>(IBAN zonder spaties)</td>
                <td colspan="3">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBanknummer" ErrorMessage="Vul een banknummer in a.u.b."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtBanknummer" ErrorMessage="De invoer voldoet niet aan de IBAN eisen" ValidationExpression="[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>E-Mail :</td>
                <td colspan="3"><asp:TextBox runat="server" Width="100%" ID="txtEmail"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Vul een E-mail adres in a.u.b."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Vul een geldig email adres in" ValidationExpression="[a-zA-Z0-9._%-]+@[a-zA-Z0-9._%-]+\.[a-zA-Z]{2,10}"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <asp:Button runat="server" Text="Volgende" OnClick="btnNext_OnClick" ID="btnNext"/>
    </div>
    </form>
</body>
</html>
