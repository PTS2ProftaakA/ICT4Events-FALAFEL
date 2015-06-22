using System;
using System.Web.Security;

namespace ICT4Events
{
    public partial class Inloggen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string user = userInput.Value.Trim();
                if (Convert.ToInt32(new Database.Database().ExecuteScalarQuery("SELECT isGebruikerGeactiveerd('{0}') FROM DUAL", user)) == 1)
                {
                    resultMessage.InnerText = String.Empty;

                    if (ActiveDirectory.AuthenticateUser(user, passInput.Value))
                    {
                        resultMessage.InnerText = String.Empty;

                        FormsAuthentication.RedirectFromLoginPage(user, true);
                    }
                    else
                    {
                        resultMessage.InnerText = "Gebruikersnaam/wachtwoord combinatie is incorrect of dit account bestaat niet";
                    }
                }
                else if (!String.IsNullOrEmpty(user))
                {
                    resultMessage.InnerText = "Dit account bestaat niet of is nog niet geactiveerd";
                }
            }
        }
    }
}