using System;
using ICT4Events.Models;

namespace ICT4Events
{
    public partial class RegistratieGegevens : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnNext.ServerClick += (o, e2) =>
            {
                //Maak een nieuw product aan met de gegevens zoals die opgegeven zijn op het formulier
                //Alle controles ofdat een waarde correct is worden uitevoerd in het formulier door middel van Validators
                Persoon persoon = new Persoon(1, txtVoorNaam.Value, txtTussenvoegsel.Value, txtAchterNaam.Value,
                    txtStraatNaam.Value, txtWoonplaats.Value, txtHuisnummer.Value, txtBanknummer.Value);

                //Sla deze persoon op in een sessie zodat deze gegevens later in de database gezet kunnen worden
                Session["Persoon"] = persoon;

                //Navigeer verder naar GUI_NieuweGebruiker_Kaart (PLEK RESERVEREN)
                Response.Redirect("./RegistratieKaart.aspx");
            };
        }
    }
}