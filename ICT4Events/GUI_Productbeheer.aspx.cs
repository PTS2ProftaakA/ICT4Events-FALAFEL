using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ICT4Events.Controllers;
using ICT4Events.Models;

namespace ICT4Events
{
    public partial class GUI_Productbeheer : System.Web.UI.Page
    {
        private ProductBeheerController _productBeheerController;

        //Lijsten om de objecten in op te slaan.
        private List<ProductCat> productCategorien;
        private List<Product> producten;
        private List<ProductExemplaar> productExemplaren;
        private List<Verhuur> verhuringen; 

        protected void Page_Load(object sender, EventArgs e)
        {
            //Controller om de data tussen deze en de database laag te regelen.
            _productBeheerController = new ProductBeheerController();

            if (!Page.IsPostBack)
            {
                //Als de pagina ververst word worden alle lijsten gevuld.
                productCategorien = _productBeheerController.KrijgAlleCategorieen();
                producten = _productBeheerController.KrijgAlleProducten(productCategorien);
                productExemplaren = _productBeheerController.KrijgAlleProductExemplaren(producten);
                verhuringen = _productBeheerController.KrijgAlleHuidigeVerhuringen(productExemplaren);

                //De dropdownlist van de producten word gevuld.
                ddlProducten.Items.Add(new ListItem("Een nieuw product toevoegen...", "0"));

                for (int i = 0; i < producten.Count(); i++)
                {
                    ddlProducten.Items.Add(new ListItem(producten[i].Merk + " " + producten[i].Serie, producten[i].ID.ToString()));
                }

                //De dropdownlist van de categorien word gevuld.
                ddlProductCategorien.Items.Add(new ListItem("Selecteer een categorie...", "0"));

                for (int i = 0; i < productCategorien.Count(); i++)
                {
                    ddlProductCategorien.Items.Add(new ListItem(productCategorien[i].Naam, productCategorien[i].ID.ToString()));
                }

                //De dropdownlist van de productexemplaren word gevuld.
                ddlProductExemplaren.Items.Add(new ListItem("Selecteer een exemplaar...", "0"));

                for (int i = 0; i < productExemplaren.Count(); i++)
                {
                    Verhuur verhuurd = verhuringen.Find(verhuur => verhuur.ProductExemplaar == productExemplaren[i]);

                    if (verhuurd == null)
                    {
                        ddlProductExemplaren.Items.Add(
                            new ListItem(
                                productExemplaren[i].Product.Merk + " " + productExemplaren[i].Product.Serie + " " +
                                productExemplaren[i].Volgnummer, productExemplaren[i].ID.ToString()));
                    }
                }

                //De dropdownlist van de verhuringen word gevuld.
                ddlVerhuringen.Items.Add(new ListItem("Selecteer een verhuring...", "0"));

                for (int i = 0; i < verhuringen.Count(); i++)
                {
                    ddlVerhuringen.Items.Add(new ListItem(verhuringen[i].ProductExemplaar.ID + " " + verhuringen[i].ProductExemplaar.Product.Merk + " " + verhuringen[i].ProductExemplaar.Product.Serie + " \t" + verhuringen[i].DatumIn.ToString() + " \t" + verhuringen[i].DatumUit.ToString(), verhuringen[i].ID.ToString()));
                }

                //De lijsten worden opgeslagen in de sessie.
                if (producten != null)
                {
                    Session["producten"] = producten;
                }
                if (productCategorien != null)
                {
                    Session["productCategorien"] = productCategorien;
                }
                if (productExemplaren != null)
                {
                    Session["productExemplaren"] = productExemplaren;
                }
                if (verhuringen != null)
                {
                    Session["verhuringen"] = verhuringen;
                }

                //Het event waarbij de producten gewisseld worde wor aangeroepen.
                //Dit zorgt ervoor dat de data in de textboxen geladen word.
                ddlProducten_OnSelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        protected void ddlProducten_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Kijken of de sessie van de producten nog bestaat.
            if(Session["producten"] != null)
            {
                //Het geselcteerde product word gezicht in de lijst.
                producten = Session["producten"] as List<Product>;

                Product geselecteerdProduct =
                    producten.Find(product => product.ID == Convert.ToInt32(ddlProducten.SelectedItem.Value));

                //Als er een product is geselecteerd worden de textboxen gevuld met de bijbehorende data.
                if (geselecteerdProduct != null)
                {
                    ddlProductCategorien.SelectedValue = geselecteerdProduct.ProductCategorie.ID.ToString();

                    tbMerk.Text = geselecteerdProduct.Merk;
                    tbSerie.Text = geselecteerdProduct.Serie;
                    tbPrijs.Text = geselecteerdProduct.Prijs.ToString();
                    tbTypeNummer.Text = geselecteerdProduct.Typenummer.ToString();

                    btnAanpassenMaken.Text = "Aanpassen";
                }
                //als de dropdownlist staat op een nieuw product aanmaken word de data leeg gemaakt.
                else
                {
                    ddlProductCategorien.SelectedValue = "0";

                    tbMerk.Text = "";
                    tbSerie.Text = "";
                    tbPrijs.Text = "";
                    tbTypeNummer.Text = "";

                    btnAanpassenMaken.Text = "Aanmaken";
                }
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnVerwijderen_OnClick(object sender, EventArgs e)
        {
            //Kijken of de sessie van de producten nog bestaat.
            if (Session["producten"] != null)
            {
                //Het geselcteerde product word gezicht in de lijst.
                producten = Session["producten"] as List<Product>;

                Product geselecteerdProduct =
                    producten.Find(product => product.ID == Convert.ToInt32(ddlProducten.SelectedItem.Value));

                //Als er een product gevonden word, word deze verwijderd en word de pagina ververst.
                if (geselecteerdProduct != null)
                {
                    _productBeheerController.VerwijderProduct(geselecteerdProduct);
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnAanpassenMaken_OnClick(object sender, EventArgs e)
        {
            //Kijken of de lijst van producten nog bestaat.
            if (Session["producten"] != null)
            {
                if (Page.IsValid)
                {
                    producten = Session["producten"] as List<Product>;
                    productCategorien = Session["productCategorien"] as List<ProductCat>;

                    //Kijken of het product terug te vinden is in de lijst van producten.
                    Product geselecteerdProduct =
                        producten.Find(product => product.ID == Convert.ToInt32(ddlProducten.SelectedItem.Value));

                    //Als het product gevonden word, word het aangepast met de ingevulde data.
                    if (geselecteerdProduct != null)
                    {
                        geselecteerdProduct.ProductCategorie = productCategorien.Find(categorie => categorie.ID == Convert.ToInt32(ddlProductCategorien.SelectedItem.Value));
                        geselecteerdProduct.Merk = tbMerk.Text;
                        geselecteerdProduct.Serie = tbSerie.Text;
                        geselecteerdProduct.Prijs = Convert.ToInt32(tbPrijs.Text);
                        geselecteerdProduct.Typenummer = Convert.ToInt32(tbTypeNummer.Text);

                        _productBeheerController.AanpassenProduct(geselecteerdProduct);
                    }
                    else
                    {
                        _productBeheerController.ToevoegenProduct(Convert.ToInt32(ddlProductCategorien.SelectedItem.Value), tbMerk.Text, tbSerie.Text, Convert.ToInt32(tbPrijs.Text), Convert.ToInt32(tbTypeNummer.Text));
                        //Als het niet teruggevonden word, word het aangemaakt met de ingevulde data.
                    }
                }
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnExemplaarAanmaken_OnClick(object sender, EventArgs e)
        {
            //Kijken of de lijst van producten nog bestaat.
            if (Session["producten"] != null)
            {
                producten = Session["producten"] as List<Product>;

                //Als de lijst bestaat word er in die lijst gezocht naar het geselecteerde product.
                Product geselecteerdProduct =
                    producten.Find(product => product.ID == Convert.ToInt32(ddlProducten.SelectedItem.Value));

                //Als het product gevonden is worden er exemplaren van aangemaakt.
                if (geselecteerdProduct != null)
                {
                    for (int i = 0; i < Convert.ToInt32(tbAantalAanmaken.Text); i++)
                    {
                        _productBeheerController.ToevoegenExemplaar(geselecteerdProduct);
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnExemplaarVerwijderen_OnClick(object sender, EventArgs e)
        {
            //Kijken of de lijst met productexemplaren nog bestaat.
            if (Session["productExemplaren"] != null)
            {
                productExemplaren = Session["productExemplaren"] as List<ProductExemplaar>;

                //Zoeken in de lijst met de exemplaren naar het geselecteerde exemplaar.
                ProductExemplaar geselecteerdProductExemplaar =
                    productExemplaren.Find(
                        productExemplaar =>
                            productExemplaar.ID == Convert.ToInt32(ddlProductExemplaren.SelectedItem.Value));

                //Als die gevonden is, word het exemplaar verwijderd.
                if (geselecteerdProductExemplaar != null)
                {
                    _productBeheerController.VerwijderProductExemplaar(geselecteerdProductExemplaar);
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnVerhuringVerwijderen_OnClick(object sender, EventArgs e)
        {
            //Kijke of de lijst mer verhuringen nog bestaat.
            if (Session["verhuringen"] != null)
            {
                verhuringen = Session["verhuringen"] as List<Verhuur>;

                //Kijken of de geselecteerde verhuring bestaat in de lijst.
                Verhuur geselecteerdeVerhuring =
                    verhuringen.Find(verhuur => verhuur.ID == Convert.ToInt32(ddlVerhuringen.SelectedItem.Value));

                //Als die gevonden is word hij verwijderd.
                if (geselecteerdeVerhuring != null)
                {
                    _productBeheerController.VerwijderVerhuring(geselecteerdeVerhuring);
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnInleveren_OnClick(object sender, EventArgs e)
        {
            //Kijken of de lijst met verhuringen bestaat.
            if (Session["verhuringen"] != null)
            {
                verhuringen = Session["verhuringen"] as List<Verhuur>;

                //Kijken of de geselecteerde verhuring bestaat in de lijst.
                Verhuur geselecteerdeVerhuring =
                    verhuringen.Find(verhuur => verhuur.ProductExemplaar.Barcode == tbBarCodeScanner.Text);

                //De Verhuring word verwijderd.
                if (geselecteerdeVerhuring != null)
                {
                    _productBeheerController.VerwijderVerhuring(geselecteerdeVerhuring);

                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    //Als de barcodesanner iets scant waarvan het niet verhuurd is zal hij een error geven via tekst.
                    tbBarCodeScanner.Text = "Deze barcode is niet terug gevonden";
                }
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}