using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events.Conrtoller;
using ICT4Events.Controllers;
using ICT4Events.Models;
using ICT4Events.UserControls;
using Oracle.ManagedDataAccess.Client;

namespace ICT4Events
{
    public partial class GUI_NieuweGebruiker_Samenvatting : System.Web.UI.Page
    {
        private Persoon persoon;
        private List<Plek> plekkenLijst;
        private List<BestelRegelBeheerder> productenLijst;
        private List<Account> accountLijst; 

        protected void Page_Load(object sender, EventArgs e)
        {
            //Vind de persoonsgegevens
            persoon = Session["Persoon"] as Persoon;

            //Vind de gereserveerde plekken
            plekkenLijst = Session["Plekken"] as List<Plek>;

            //Vind de producnten in de winkelwagen
            productenLijst = Session["winkelwagen"] as List<BestelRegelBeheerder>;
            
            //Vind de accounts die aangemaakt moeten worden
            accountLijst = Session["Accounts"] as List<Account>;

            //Vul de volledige naam in
            lblPersoonNaam.Text = persoon.Voornaam + " " + persoon.Tussenvoegsel + " " + persoon.Achternaam;

            //Vul de adresgevens is
            lblAdres.Text = persoon.Straat + " " + persoon.Huisnr;

            //Vul de woonplaats gegevens
            lblWoonplaats.Text = persoon.Woonplaats;

            //Vul de bangegevens
            lblBanknummer.Text = persoon.Banknr;

            //Ga elke plek langs die je wil reserveren
            foreach (var plek in plekkenLijst)
            {
                //Vul een item voor elke gevonden plek
                plaatsLijst.Items.Add("Plek" +plek.Nummer);
            }
 
            // Ga alle producten na die je wil reserveren
            foreach (var product in productenLijst)
            {
                //Vul een item voor elk product
                productLijst.Items.Add(product.ProductNaam);
            }

            //Ga elk account na
            foreach (var account in accountLijst)
            {
                //Vul een item voor elk persoon
                Vrienden.Items.Add(account.Email + " - " + account.Gebruikersnaam);
            }
        }

        protected void btnBevestig_OnClick(object sender, EventArgs e)
        {
            Database.Database database = new Database.Database(); 
            ReserveringController controller =  new ReserveringController();

            controller.VoegPersoonEnReserveringToe(persoon, 1);

            var rand = new Random();
            var emailController = new EmailController();

            foreach (var account in accountLijst)
            {
                string activatiehash = new string(Enumerable.Repeat("AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789", 20).Select(s => s[rand.Next(s.Length)]).ToArray());
                account.Activatiehash = activatiehash;
                controller.VoegAccountsToeAanReservering(account);

                ActiveDirectory.AddUser(account.Gebruikersnaam, account.Email, "Asdf4sdf");

                emailController.eMail(account.Activatiehash, account.Email, account.Gebruikersnaam, "98723923587");
            }

            foreach (var plek in plekkenLijst)
            {
                controller.KoppelReserveringAanPlaatsen(plek.ID);
            }

            List<ProductExemplaar> exemplaren =
                controller.KrijgAlleProductExemplaren(Product.ZoekAlleProducten(database));

            Event _event = Event.ZoekEventMetID(database, 1);

            foreach (BestelRegelBeheerder bestelRegel in productenLijst)
            {
                List<ProductExemplaar> tempExemplaren =
                    exemplaren.FindAll(exemplaar => exemplaar.Product.ID == bestelRegel.ProductID);

                for (int i = 0; i < bestelRegel.HuidigeHoeveelheid; i++)
                {
                    controller.ReserveerMateriaal(tempExemplaren[i], _event);
                }
            }

            Response.Redirect("Inloggen.aspx");
        }
    }
}