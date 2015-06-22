<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GUI_NieuweGebruiker_Kaart.aspx.cs" Inherits="ICT4Events.GUI_NieuweGebruiker_Kaart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="dropPlaatsen" runat="server" AutoPostBack="True">
                    </asp:DropDownList><br />
                    <asp:panel runat="server" BorderStyle="Solid" BorderWidth="1px" ScrollBars="None">
                        <asp:Label runat="server">Maximale hoeveelheid personen</asp:Label><br />
                        <asp:TextBox runat="server" ID="txtHoeveelheidPersonen" Enabled="False">6</asp:TextBox><br />
                        <br />
                        <asp:panel runat="server" BorderStyle="Solid" BorderWidth="1px" ScrollBars="Vertical" Height="200px" ID="pnlSpecificaties">
               
                        </asp:panel>
                    </asp:panel>

                    <asp:Panel runat="server" BorderStyle="Solid" BorderWidth="1px" ScrollBars="None">
                        <asp:Label runat="server" >
                            Uw gekozen plaatsen 
                        </asp:Label><br />
                        <asp:panel runat="server" BorderStyle="Solid" BorderWidth="1px" ScrollBars="Vertical" Height="100px">
                            <asp:CheckBoxList ID="dropTeHurenPlaatsen" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                    </asp:Panel>
                    <asp:Button runat="server" ID="btnVolgende" Text="Volgende" OnClick="btnVolgende_OnClick"/><br />
                    <asp:Label runat="server" ID="lblError">Je moet minstens één plaats kiezen</asp:Label>
                </td>
                <td>
                    <asp:Image runat="server" ImageUrl="~/Images/Camping_ReeënDal.png" Height="500px"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
