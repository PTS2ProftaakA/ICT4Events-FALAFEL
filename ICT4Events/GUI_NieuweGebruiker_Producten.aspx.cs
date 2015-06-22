using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events.Conrtoller;
using ICT4Events.Models;
using ICT4Events.UserControls;

namespace ICT4Events
{
    public partial class GUI_NieuweGebruiker_Producten : System.Web.UI.Page
    {
        //Maak een lijst die overal bereikbaar is om de producten in de zoekbalk en in de winkelwagen bij te houden 
        //0 = Zoekresultaten
        //1 = Winkelwagen
        private List<Product>[] producten;
        private List<ProductExemplaar> productExemplaren; 

        //Wanneer er een aanvraag gedaan wordt naar de pagina
        protected void Page_Load(object sender, EventArgs e)
        {
            //Wanneer de pagina voor het eerst geladen wordt
            if (!IsPostBack)
            {
                //Vul een instantie van de database om daarop een query uit te voeren
                Database.Database database = new Database.Database();
                //instantieer de productenlijst
                producten = new List<Product>[2];

                //Vul de eerste lijst met de alle producten
                producten[0] = Product.ZoekAlleProducten(database);
                //Vul de winkelwagen in eerste instantie niet
                producten[1] = new List<Product>();

                //Vul de sessie met de productenlijst
                Session["Productverhuur"] = producten;

                //Vind een evenement om de datum van op te vragen
                Event _event = Event.ZoekEventMetID(database, 1);
                txtHuurDatum.Text = "Je huurt de producten vanaf " + _event.DatumStart.Day + "-" + _event.DatumStart.Month + "-" + _event.DatumStart.Year + 
                                    " tot en met " +
                                    _event.DatumEinde.Day + "-" + _event.DatumEinde.Month + "-" + _event.DatumEinde.Year + ".";

                ReserveringController controller = new ReserveringController();
                productExemplaren = controller.KrijgAlleProductExemplaren(producten[0]);
                Session["Exemplaren"] = productExemplaren;
            }

            productExemplaren = Session["Exemplaren"] as List<ProductExemplaar>;
            //Ververs de GUI
            Refresh();
        }

        private void Refresh()
        {
           //Hou een variabele bij om te kijken of je in de eerste of in te tweede lijst bezig bent
            int i = 0;

            //Maak beide panels eerst leeg
            pnlProducten.Controls.Clear();
            pnlWinkelwagen.Controls.Clear();

            //Maak een lijst van bestelregels(Custom control)
            List<BestelRegelBeheerder> bestelRegelLijst = new List<BestelRegelBeheerder>();

            //Ga door beide lijsten om de juiste panels te vullen
            foreach (List<Product> productLijst in Session["Productverhuur"] as List<Product>[])
            {
                //Ga per lijst door alle producten heen
                foreach (Product product in productLijst)
                {
                    //Dit is een witregel die je aan een panel toe kan voegen wanneer je een witregel wil
                    Literal litBreak = new Literal();
                    litBreak.Text = "<br/>";

                    //Wanneer je door de eerste lijst geen gaat(ZOEKRESULTATEN)
                    if (i == 0)
                    {
                        //Maak een tekst aan bestaande uit het merk en de serie
                        Literal literal = new Literal();
                        literal.Text = product.Merk + " " + product.Serie;

                        //Maak een knop aan om dit product toe te voegen aan de winkelwagen
                        Button button = new Button();
                        button.ID = product.ID.ToString();
                        button.Text = "+";
                        button.Click += buttonVoegToe_Click;

                        //Wanneer er al een sessie bestaat voor producten
                        if (Session["Productverhuur"] != null)
                        {
                            //Vul dan een lijst van producten met die sessie
                            var Winkelwagenproducten = Session["Productverhuur"] as List<Product>[];
                            //Ga elk procut langs
                            foreach (var productinwinkelwagen in Winkelwagenproducten[1])
                            {
                                //Maak de button om een zoekresultaat in de winkelwagen onbruikbaar als dat product al in de winkelwagen zit
                                if (productinwinkelwagen.ID == product.ID)
                                {
                                    button.Enabled = false;
                                }
                            }
                        }
                        //Kijk of de ingevoerde waarden in hoofdletters gelijk zijn aan een bestaand product
                        if (product.Merk.ToUpper().Contains(txtZoek.Text.Trim().ToUpper()) 
                         || product.Serie.ToUpper().Contains(txtZoek.Text.Trim().ToUpper()))
                        {
                           //Voeg de Contols toe voor de gevonden producten
                            pnlProducten.Controls.Add(literal);
                            pnlProducten.Controls.Add(button);
                            pnlProducten.Controls.Add(litBreak);
                        }
                    }
                    //Wanneer je door de tweede lijst heen gaat(WINKELWAGEN)
                    else if (i == 1)
                    {
                        //Meek een lege bestelregel aan
                        BestelRegelBeheerder bestelRegel = null;
                        //Kijk voor elke bestelregel in de lijst
                        foreach (var bestelregel in Session["winkelwagen"] as List<BestelRegelBeheerder>)
                        {
                            //Wanneer het product overeen komt met een bestaande bestelregel
                            if (bestelregel.ProductID == product.ID)
                            {
                                //Maak dan een bestelregel aan met een bestaande hoeveelheid
                                bestelRegel = new BestelRegelBeheerder(bestelregel.HuidigeHoeveelheid, product.ID, product.Merk + " " + product.Serie,bestelregel.MaxHoeveelheid);
                            }
                        }
                        //Wanneer de bestelregel nog niet gevuld is, vul dan een compleet nieuwe bestelregel
                        if (bestelRegel == null)
                        {
                            bestelRegel = new BestelRegelBeheerder(1, product.ID, product.Merk + " " + product.Serie,productExemplaren.Where(exemplaar => exemplaar.Product.ID == product.ID).Count());
                        }

                        //Koppel de events aan de knoppen
                        bestelRegel.BtnMin.Click += BtnMin_Click;
                        bestelRegel.BtnPlus.Click += BtnPlus_Click;
                        bestelRegel.BtnVerwijder.Click += BtnVerwijder_Click;

                        //Voeg de bestelregel toe aan de lijst van bestelregels
                        bestelRegelLijst.Add(bestelRegel);

                        //Vul de panels
                        pnlWinkelwagen.Controls.Add(bestelRegel);
                        pnlWinkelwagen.Controls.Add(litBreak);
                    }

                }
                //Wanneer de eerste is geweest komt de tweede dus int 0 wordt 1
                i++;
                // Wanneer er geen producten zijn in de winkelwage verschijnt er een tekst in de winkelwagen dat de winkelwagen leeg is
                if (productLijst.Count() < 1)
                {
                    pnlWinkelwagen.Controls.Add(new Literal { Text = "Er zijn zitten geen producten in de winkelwagen." });
                }
            }
            //Vul een sessie voor de winkelwagenitems
            Session["winkelwagen"] = bestelRegelLijst;
        }

        //Wanneer er op de knop wodt geklikt om een product aan de winkelwagen toe te voegen
        void buttonVoegToe_Click(object sender, EventArgs e)
        {
            //Vul een productenlijst met bestaande producten
            producten = Session["Productverhuur"] as List<Product>[];

            //Ga elk prouct langs in de zoekresultaten
            foreach (var product in producten[0])
            {
                //Controlleer of er op de juiste knop wordt gedrukt
                if (product.ID == Convert.ToInt32(((Button)sender).ID))
                {
                    //Voeg een product toe aan de winkelwagen
                    producten[1].Add(product);
                }
            }
            //Vul een sessie met de nieuwe gegevens van producten
            Session["Productverhuur"] = producten;
            //Ververs de pagina
            Refresh();
        }

        void BtnVerwijder_Click(object sender, EventArgs e)
        {
            //Vul een lijst van bestelregels met een sessie van alle bestelregel in de winkelwagen
            List<BestelRegelBeheerder> bestelRegelLijst = Session["winkelwagen"] as List<BestelRegelBeheerder>;
            //Vul een lijst van alle lijsten van producten
            producten = Session["Productverhuur"] as List<Product>[];
            //Ga alle bestelregels langs
            foreach (var bestelRegel in bestelRegelLijst)
            {
                //Als de ID van de knop overeenkomt met de knop waarop geklikt is
                if ("btnVerwijder" + bestelRegel.ProductID == ((Button)sender).ID)
                {
                    //Verwijder een product uit de winkelwagen
                    producten[1].RemoveAll(p => p.ID == bestelRegel.ProductID);
                }
            }
            //Verwijder de user control uit de winkelwagenlijst
            bestelRegelLijst.RemoveAll(b => b.ID == ((Button) sender).ID);
            //Vul de lijsten weer in in de bijbehorende sessies
            Session["Productverhuur"] = producten;
            Session["winkelwagen"] = bestelRegelLijst;

            // Ververs de pagina
            Refresh();
        }

        void BtnPlus_Click(object sender, EventArgs e)
        {
            //Maak een lijst van bestelregels 
            List<BestelRegelBeheerder> winkelwagen = new List<BestelRegelBeheerder>();
            //Ga elke bestelregel langs in de sessie
            foreach (var bestelRegel in Session["winkelwagen"] as List<BestelRegelBeheerder>)
            {
                //Wanneer de knop waarop geklikt is gelijk is aan het id van de user control
                if (bestelRegel.BtnPlus == (Button)sender)
                {
                    if (bestelRegel.HuidigeHoeveelheid < bestelRegel.MaxHoeveelheid)
                    {
                        //Hoog de hoeveelheid exemplaren op
                        bestelRegel.HuidigeHoeveelheid++;
                        //Pas de tekst aan
                        bestelRegel.Hoeveelheid.Text = bestelRegel.HuidigeHoeveelheid.ToString();
                    }
                }
                //Voeg de bestelregel toe aan de lijst
                winkelwagen.Add(bestelRegel);
            }
            //Update de sessie
            Session["winkelwagen"] = winkelwagen;
        }

        void BtnMin_Click(object sender, EventArgs e)
        {
            //Maak een lijst van bestelregels 
            List<BestelRegelBeheerder> winkelwagen = new List<BestelRegelBeheerder>();
            //Ga elke bestelregel langs in de sessie
            foreach (var bestelRegel in Session["winkelwagen"] as List<BestelRegelBeheerder>)
            {
                //Wanneer de knop waarop geklikt is gelijk is aan het id van de user control
                if (bestelRegel.BtnMin == (Button)sender)
                {
                    if (bestelRegel.HuidigeHoeveelheid >1)
                    {
                        //Hoog de hoeveelheid exemplaren op
                        bestelRegel.HuidigeHoeveelheid--;
                        //Pas de tekst aan
                        bestelRegel.Hoeveelheid.Text = bestelRegel.HuidigeHoeveelheid.ToString();
                    }
                }
                //Voeg de bestelregel toe aan de lijst
                winkelwagen.Add(bestelRegel);
            }
            //Update de sessie
            Session["winkelwagen"] = winkelwagen;
        }

        protected void txtZoek_OnTextChanged(object sender, EventArgs e)
        {
            //Ververs de pagina
            Refresh();
        }

        protected void btnVerder_OnClick(object sender, EventArgs e)
        {
            //Navigeer naar de volgende pagina (Acountgegevens)
            Response.Redirect("GUI_NieuweGebruiker_Vrienden.aspx");
        }
    }
}