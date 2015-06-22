using System;
using ICT4Events.Models;

namespace ICT4Events
{
    public partial class GUI_NieuweGebruiker_Gegevens : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnNext_OnClick(object sender, EventArgs e)
        {
            //Maak een nieuw product aan met de gegevens zoals die opgegeven zijn op het formulier
            //Alle controles ofdat een waarde correct is worden uitevoerd in het formulier door middel van Validators
            Persoon persoon = new Persoon(1, txtVoorNaam.Text, txtTussenvoegsel.Text, txtAchterNaam.Text,
                txtStraatNaam.Text, txtWoonplaats.Text, txtHuisnummer.Text, txtBanknummer.Text);

            //Sla deze persoon op in een sessie zodat deze gegevens later in de database gezet kunnen worden
            Session["Persoon"] = persoon;

            //Navigeer verder naar GUI_NieuweGebruiker_Kaart (PLEK RESERVEREN)
            Response.Redirect("./GUI_NieuweGebruiker_Kaart.aspx");
        }
    }
}