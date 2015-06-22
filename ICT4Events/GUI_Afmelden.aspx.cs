using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events.Controllers;

namespace ICT4Events
{
    public partial class GUI_Afmelden : System.Web.UI.Page
    {
        private MasterController _masterController;

        protected void Page_Load(object sender, EventArgs e)
        {
            _masterController = new MasterController();
        }

        protected void btnVerwijderAccount_OnClick(object sender, EventArgs e)
        {
            //Huidige gebruiker word opgehaald.
            if (HttpContext.Current.User.Identity.Name != null)
            {
                //Eventuele error bericht word opgehaald.
                string bericht = _masterController.VerwijderAccount(HttpContext.Current.User.Identity.Name);

                //Het bericht word omgezet.
                if (bericht != "")
                {
                    lblWaarschuwing.Text = bericht.Substring(bericht.IndexOf(":") + 2,
                        bericht.IndexOf(".") - bericht.IndexOf(":"));
                }
                //Als het een leeg bericht was word de gebruiker uigelogd en verwezen naar het inlogscherm.
                else
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("Inloggen.aspx");
                }
            }
        }
    }
}