using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ICT4Events.Models;

namespace ICT4Events
{
    public partial class GUI_NieuweGebruiker_Vrienden : System.Web.UI.Page
    {
        //Maak lijsten van tekstvelden die je in moet vullen en controleren
        private List<TextBox> emailLijst;
        private List<TextBox> gebruikersnaamLijst;

        //Maak een lijst van accounts
        private List<Account> accounts; 

        protected void Page_Load(object sender, EventArgs e)
        {
            //Wanneer je voor het eerst deze pagina bezoeks
            if (!IsPostBack)
            {
                //Maak een nieuwe instantie van de database
                Database.Database database = new Database.Database();
                //Vul de lijst van accounts met alle accounts
                accounts = Account.ZoekAlleGebruikers(database);
                //Vul een sessie met de accounts
                Session["Accounts"] = accounts;
            }
            //Wanneer accounts sessie bestaat
            if (Session["Accounts"] != null)
            {
                //Vul de lijst van accounts
                accounts = Session["Accounts"] as List<Account>;
            }
            //Instantieer de lijst van email tekstvelden
            emailLijst = new List<TextBox>();
            //Instantieer de lijst van gebruikersnaam tekstvelden
            gebruikersnaamLijst = new List<TextBox>();

            List<Plek> plekkenLijst = Session["Plekken"] as List<Plek>;
            int hoeveelheidTextboxen = 0;
            foreach (Plek plek in plekkenLijst)
            {
                hoeveelheidTextboxen += plek.Capaciteit;
            }
            //Voer net zo lang tekstvelden in als de lengte van de loop
            for (int i = 0; i < hoeveelheidTextboxen; i++)
            {
                //Voeg de controls toe
                pnlGegevens.Controls.Add(NieuweInvoerVelden(i));
            }
        }

        private Panel NieuweInvoerVelden(int ID)
        {
            //Maak een nieuwe panel
            Panel nieuweInvoervelden = new Panel();

            //Maak een listeral die een witregel zet in de panel
            Literal litBreak = new Literal {Text = "<br/>"};

            //Maak een tekst aan om bij de email tekstvelden te laten zien
            Literal litEmail = new Literal {Text = "Email :"};

            //Maak een nieuw tekstveld aan om een email in te typen
            TextBox txtEmail = new TextBox();
            txtEmail.ID = "txtEmail" + ID;

            //Maak een tekst aan om bij de email tekstvelden te laten zien
            Literal litGebruikersnaam = new Literal {Text = "Gebruikernaam :"};

            //Maak een nieuw tekstveld aan om een gebruikersnaam in te typen
            TextBox txtGebruikersnaam = new TextBox();
            txtGebruikersnaam.ID = "txtGebruikernaam" + ID;

            //Voeg de controls toe
            nieuweInvoervelden.Controls.Add(litEmail);
            nieuweInvoervelden.Controls.Add(litBreak);
            nieuweInvoervelden.Controls.Add(txtEmail);
            nieuweInvoervelden.Controls.Add(litBreak);
            nieuweInvoervelden.Controls.Add(litGebruikersnaam);
            nieuweInvoervelden.Controls.Add(litBreak);
            nieuweInvoervelden.Controls.Add(txtGebruikersnaam);
            nieuweInvoervelden.Controls.Add(litBreak);

            //Voeg de tekstvelden in de lijst van controls
            emailLijst.Add(txtEmail);
            gebruikersnaamLijst.Add(txtGebruikersnaam);

            //Geef de volle panel terug
            return nieuweInvoervelden;
        }

        protected void btnVerder_OnClick(object sender, EventArgs e)
        {
            //Zet de foutmelding uit
            lblError.Text = "";

            //Vind elk account
            foreach (Account account in accounts)
            {
                //Ga de tekst na die je wil controleren
                foreach (TextBox textboxToCompare in emailLijst)
                {
                    //Als er een veld leeg is, geef dan een foutmelding
                    if (textboxToCompare.Text == "")
                    {
                        lblError.Text = "Niet alle velden zijn gevuld";
                        return;
                    }
                    //Ga elk ander tekstveld langs
                    foreach (TextBox textboxToCompareTo in emailLijst)
                    {
                        //Wanneer je je met jezelf wil vergelijken
                        if (textboxToCompare.ID == textboxToCompareTo.ID)
                        {
                            // Doe niets
                        }
                        //Wanneer twee tekstvalden dezelfde tekst hebben
                        else if (textboxToCompare.Text == textboxToCompareTo.Text)
                        {
                            //Geen een foutmelding
                            lblError.Text = "De email waarde \"" + textboxToCompare.Text + "\" is dubbel.";
                            return;
                        }
                    }
                    //Kijk of de waarde al voorkomt in de database, wanneer dit zo is, geef dan een melding
                    if (account.Email.ToUpper() == textboxToCompare.Text.ToUpper())
                    {
                        lblError.Text = "Het email adres \"" + textboxToCompare.Text + "\" bestaat al.";
                        return;
                    }
                }
                //Ga de tekst na die je wil controleren
                foreach (TextBox textboxToCompare in gebruikersnaamLijst)
                {
                    //Als er een veld leeg is, geef dan een foutmelding
                    if (textboxToCompare.Text == "")
                    {
                        lblError.Text = "Niet alle velden zijn gevuld";
                        return;
                    }
                    //Ga elk ander tekstveld langs
                    foreach (TextBox textboxToCompareTo in gebruikersnaamLijst)
                    {
                        //Wanneer je je met jezelf wil vergelijken
                        if (textboxToCompare.ID == textboxToCompareTo.ID)
                        {
                            // Doe niets
                        }
                        //Wanneer twee tekstvalden dezelfde tekst hebben
                        else if (textboxToCompare.Text == textboxToCompareTo.Text)
                        {
                            //Geen een foutmelding
                            lblError.Text = "De gebruikersnaam waarde \"" + textboxToCompare.Text + "\" is dubbel.";
                            return;
                        }
                    }
                    //Kijk of de waarde al voorkomt in de database, wanneer dit zo is, geef dan een melding
                    if (account.Gebruikersnaam.ToUpper() == textboxToCompare.Text.ToUpper())
                    {
                        lblError.Text = "De gebruikersnaam \"" + textboxToCompare.Text + "\" bestaat al.";
                        return;
                    }
                }
            }

            //Vul een nieuwe lijst met accounts
            List<Account> doorgevenAccounts = new List<Account>();

            //Zoek door alle velden
            for (int i = 0; i < emailLijst.Count; i++)
            {
                //Voeg alle tekstveldwaarden toe aan de lijst
                doorgevenAccounts.Add(new Account(i, gebruikersnaamLijst[i].Text, emailLijst[i].Text, null, false));
            }

            //Maak een sessie van accounts aan
            Session["Accounts"] = doorgevenAccounts;

            //Navigeer naar de overzichtspagina
            Response.Redirect("GUI_NieuweGebruiker_Samenvatting.aspx");
        }
    }
}