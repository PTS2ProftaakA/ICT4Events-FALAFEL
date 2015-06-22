using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events.Models;

namespace ICT4Events
{
    public partial class GUI_NieuweGebruiker_Kaart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Maak een instantia aan van de database die gebruikt zal gaan worden om de juiste waarden op te halen 
            Database.Database database = new Database.Database();
            
            //Maak eerst de Panel pnlSpecificaties leeg om dubbele waarden te voorkomen
            pnlSpecificaties.Controls.Clear();

            //Dit is de code die alleen uitgevoerd wordt wanneer de pagine voor het eerste aangeroepen wordt
            if (!IsPostBack)
            {
                //vul de Dropdownlist DropPlaatsen met de bischikbare plaatsen
                foreach (Models.Plek plek in Models.Plek.GeefBeschikbarePlekken(database))
                {
                    //Maak voor elke plaats een ListItem aan zodat je deze een tekst en een waarde kan geven
                    //De tekst laat je zien en de waarden wordt teruggegeven wanneer daarom gevraagd wordt
                    ListItem li = new ListItem(plek.ToString(),plek.Nummer.ToString());
                    //Voeg het item toe aan de dropdownlost
                    dropPlaatsen.Items.Add(li);
                    //Voeg diezelfde items ook toe aan de checkboxlist van de plakken die je wil reserveren
                    dropTeHurenPlaatsen.Items.Add(li);
                }
                //Maak de foutmelding onzichtbaar
                lblError.Visible = false;
                
            }

            //Zoek in de DropDownList naar de geselecteerde plaats en zoek alle specificaties die verbomden zijn aan die plaats
            foreach (Specificatie specificatie in Specificatie.GeefSpecificatiesVanplek(database, Convert.ToInt32(dropPlaatsen.SelectedValue)))
            {
                //Maak een label aan waar uiteindelijk de plekspecificaties komen kijken
                Label label = new Label();
                //Stel in wat je wil zien (bv. [Samsung] [Galaxy 10])
                label.Text = specificatie.Naam + " : " + specificatie.Waarde + "<br/>";
                //Voeg die label toe aan het panel waarin alle specificaties staan
                pnlSpecificaties.Controls.Add(label);
            }
            //Stel de tekst van de hoeveelheid plaatsen in op de hoeveelheid plaatsen van het gekozen item in de DropdownList
            txtHoeveelheidPersonen.Text = Plek.ZoekPlekMetID(database, Convert.ToInt32(dropPlaatsen.SelectedValue)).Capaciteit.ToString();
        }

        //Wanneer er op de knop "volgende wordt geklikt"
        protected void btnVolgende_OnClick(object sender, EventArgs e)
        {
            //Maak een database instantie aan waar de queries op uit gevoerd gaan worden
            Database.Database database = new Database.Database();
            //Maak een nieuwe lijst van gekozen plaatsen
            List<Plek> gekozenPlekken = new List<Plek>();

            //Controlleer voor elk item in de checkboxlist welke er geselecteed zijn en de geselecteerde items moeten toegevoegd worden aan de lijst van gekozen plekken
            foreach (ListItem listItem in dropTeHurenPlaatsen.Items)
            {
                if (listItem.Selected == true)
                {
                    gekozenPlekken.Add(Plek.ZoekPlekMetID(database,Convert.ToInt32(listItem.Value)));
                }
            }

            //Wanneer er niet eens 1 plaats gekozen is geef dan een melding dat er minstens 1 plaats gekozen moet zijn
            if (gekozenPlekken.Count() < 1)
            {
                lblError.Visible = true;
            }
            else
            {
                //Vul een sessie met de gekozen plekken
                Session["Plekken"] = gekozenPlekken;
                //Navigeer naar Materiaal vehuur
                Response.Redirect("./GUI_NieuweGebruiker_Producten.aspx");
            }
        }
    }
}