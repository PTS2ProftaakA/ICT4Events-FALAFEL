<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inloggen.aspx.cs" Inherits="ICT4Events.Inloggen" %>
<!DOCTYPE html>
<html>
    <head runat="server">
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
        <link href="Content/roboto.min.css" rel="stylesheet" />
        <link href="Content/material.min.css" rel="stylesheet" />
        <link href="Content/ripples.css" rel="stylesheet" />
        <title>ICT4Events - Inloggen</title>
    </head>
    <body>
        <form id="formInlog" runat="server">
            <div class="col-lg-4 col-lg-offset-4" style="margin-top: 100px;">
                <h2>ICT4Events</h2><br/>
                <div class="form-group <% if (IsPostBack && String.IsNullOrEmpty(userInput.Value)) Response.Write("has-error"); %>">
                    <input type="text" class="form-control" id="userInput" placeholder="Gebruikersnaam" runat="server" />
                    <label for="userInput" class="control-label">
                        <asp:RequiredFieldValidator ControlToValidate="userInput" ErrorMessage="Gebruikersnaam mag niet leeg zijn" runat="server" />
                    </label>
                </div>
                <div class="form-group <% if (IsPostBack && String.IsNullOrEmpty(passInput.Value)) Response.Write("has-error"); %>">
                    <input type="password" class="form-control" id="passInput" placeholder="Wachtwoord" runat="server" />
                    <label for="passInput" class="control-label">
                        <asp:RequiredFieldValidator ControlToValidate="passInput" ErrorMessage="Wachtwoord mag niet leeg zijn" runat="server" />
                    </label>
                </div>
                <div class="form-group">
                    <label class="control-label" id="resultMessage" runat="server"></label>
                    <input type="submit" class="btn btn-primary" style="float: right;" id="inlogSubmit" value="Inloggen" />
                    <a href="RegistratieGegevens.aspx" class="btn btn-flat btn-primary" style="float: right;">Registreren</a>
                </div>
            </div>
        </form>
        <script src="Scripts/jquery-1.9.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
        <script src="Scripts/ripples.min.js"></script>
        <script src="Scripts/material.min.js"></script>
        <script>
            $(document).ready(function() {
                $.material.init();
            });
        </script>
    </body>
</html>