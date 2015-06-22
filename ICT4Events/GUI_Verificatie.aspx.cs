using System;
using System.Web;
using ICT4Events.Controllers;

namespace ICT4Events
{
    public partial class GUI_Verificatie : System.Web.UI.Page
    {
        private MasterController _masterController;

        protected void Page_Load(object sender, EventArgs e)
        {
            _masterController = new MasterController();

            //url van de pagina word opgehaald.
            string url = HttpContext.Current.Request.Url.AbsoluteUri;

            if (url.LastIndexOf(".aspx/", StringComparison.CurrentCulture) != -1)
            {
                //Activeercode word gestript van het url.
                string activeerCode = url.Substring(url.LastIndexOf(".aspx/", StringComparison.CurrentCulture) + 6);

                //Een email wordr verstuurd met een activatielink.
                //De gebruiker word aangesproken met zijn/haar gebruikersnaam.
                _masterController.ActiveerAccount(activeerCode);
            }
        }
    }
}