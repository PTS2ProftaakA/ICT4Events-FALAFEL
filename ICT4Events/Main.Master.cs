using System;
using System.Web.Security;

namespace ICT4Events
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
                FormsAuthentication.RedirectToLoginPage();

            uitloggen.ServerClick += (o, e2) =>
            {
                FormsAuthentication.SignOut();
                Response.Redirect("Inloggen.aspx");
            };
        }
    }
}