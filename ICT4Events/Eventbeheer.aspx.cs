using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ICT4Events.Controllers;
using ICT4Events.Models;

namespace ICT4Events
{
    public partial class Eventbeheer : System.Web.UI.Page
    {
        //Controller om al het dataverkeer tussen deze en de datalaag te regelen.
        private EventBeheerController _eventBeheerController;

        //Lijsten voor de locaties en de evenementen.
        private List<Locatie> _locaties;
        private List<Event> _events;
        public Eventbeheer()
        {
            _eventBeheerController = new EventBeheerController();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //Het legen de dropdown menus.
                ddlLocaties.Items.Clear();
                ddlEvenementen.Items.Clear();

                //Het ophalen van alle locaties en evenementen.
                _locaties = _eventBeheerController.VerkrijgAlleLocaties();
                _events = _eventBeheerController.VerkrijgAlleEvents(_locaties);

                ddlLocaties.Items.Add(new ListItem("Selecteer een locatie...", "0"));

                //Elke locatie word toegevoegd aan de dropdownlist.
                foreach (Locatie locatie in _locaties)
                {
                    ddlLocaties.Items.Add(new ListItem(locatie.Naam, locatie.ID.ToString()));
                }

                ddlEvenementen.Items.Add(new ListItem("Selecteer een evenement...", "0"));

                //Elk evenement word toegevoegd aan de dropdownlist.
                foreach (Event selectedEvent in _events)
                {
                    ddlEvenementen.Items.Add(new ListItem(selectedEvent.Naam, selectedEvent.ID.ToString()));
                }

                Session["locaties"] = _locaties;
                Session["events"] = _events;
            }
        }

        protected void btnEvenementCreeren_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && Session["locaties"] != null && Session["locaties"] != null)
            {
                //Alle locaties en evenementen worden opgehaald.
                _events = Session["events"] as List<Event>;
                _locaties = Session["locaties"] as List<Locatie>;

                if (_events.Find(selectedEvent => selectedEvent.ID == Convert.ToInt32(ddlEvenementen.SelectedItem.Value)) == null)
                {
                    //De geselecteerde locatie vinden.
                    Locatie geselecteerdeLocatie =
                        _locaties.Find(locatie => Convert.ToInt32(ddlLocaties.SelectedItem.Value) == locatie.ID);

                    if (geselecteerdeLocatie != null)
                    {
                        //Een paar checks om te zorgen dat de datum wel correct is.
                        if (calStartDatum.SelectedDate.Date > calEindDatum.SelectedDate.Date)
                        {
                            lblErrorLabel.Text = "De startdatum ligt niet voor de einddatum.";
                        }
                        else if (calStartDatum.SelectedDate.Date < DateTime.Now)
                        {
                            lblErrorLabel.Text = "De startdatum ligt in het verleden.";
                        }
                        else
                        {
                            //Als alles goed is word het event aangemaakt via de controller.
                            _eventBeheerController.EventToevoegen(geselecteerdeLocatie.ID, tbEvenementNaam.Text,
                                calStartDatum.SelectedDate,
                                calEindDatum.SelectedDate, Convert.ToInt32(tbMaxAantalBezoekers.Text));

                            Response.Redirect(Request.RawUrl);
                        }
                    }
                    else
                    {
                        //pagina word herladen.
                        Response.Redirect(Request.RawUrl);
                    }
                }
                else
                {
                    _events = Session["events"] as List<Event>;

                    Event geselecteerdEvent =
                        _events.Find(
                            selectedEvent => selectedEvent.ID == Convert.ToInt32(ddlEvenementen.SelectedItem.Value));

                    if(geselecteerdEvent != null)
                    {
                        //Een check of het evenement niet bezig is voordat het aangepast word.
                        if (DateTime.Now < geselecteerdEvent.DatumEinde && DateTime.Now > geselecteerdEvent.DatumStart)
                        {
                            lblErrorLabel.Text = "Je kan het evenement niet aanpassen als het bezig is.";
                        }
                        else
                        {
                            //Als het niet bezig is word de data aangepast met de ingevoerde data.
                            lblErrorLabel.Text = "";

                            Locatie geselecteerdeLocatie =
                        _locaties.Find(locatie => Convert.ToInt32(ddlLocaties.SelectedItem.Value) == locatie.ID);

                            if (geselecteerdeLocatie != null)
                            {
                                geselecteerdEvent.Naam = tbEvenementNaam.Text;
                                geselecteerdEvent.DatumStart = calStartDatum.SelectedDate;
                                geselecteerdEvent.DatumEinde = calEindDatum.SelectedDate;
                                geselecteerdEvent.MaxBezoekers = Convert.ToInt32(tbMaxAantalBezoekers.Text);

                                _eventBeheerController.AanpassenEvent(geselecteerdEvent);
                                Response.Redirect(Request.RawUrl);
                            }
                            else
                            {
                                //pagina word herladen.
                                Response.Redirect(Request.RawUrl);
                            }
                        }
                    }
                    else
                    {
                        //pagina word herladen.
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            else
            {
                //pagina word herladen.
                Response.Redirect(Request.RawUrl);
            }
        }


        protected void btnVerwijderEvent_OnClick(object sender, EventArgs e)
        {
            //Alle evenementen worden opgehaald.
            if (Session["events"] != null)
            {
                _events = Session["events"] as List<Event>;

                Event geselecteerdEvent =
                        _events.Find(
                            selectedEvent => selectedEvent.ID == Convert.ToInt32(ddlEvenementen.SelectedItem.Value));

                if (geselecteerdEvent != null)
                {
                    //Een check of het evenement niet bezig is voordat het verwijderd word.
                    if (DateTime.Now < geselecteerdEvent.DatumEinde && DateTime.Now > geselecteerdEvent.DatumStart)
                    {
                        lblErrorLabel.Text = "Je kan het evenement niet verwijderen als het bezig is.";
                    }
                    else
                    {
                        //Als het niet nu is word het verwijderd via de controller.
                        _eventBeheerController.VerwijderEvent(geselecteerdEvent);
                        Response.Redirect(Request.RawUrl);
                    }
                }
                else
                {
                    //pagina word herladen.
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                //pagina word herladen.
                Response.Redirect(Request.RawUrl);
            }
        }


        //Validatie om te kijken of de startdatum kalendar is ingevuld.
        protected void calStartDatumValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = calStartDatum.SelectedDate != default(DateTime);

            if (args.IsValid)
            {
                lblStartDatumValidatie.Text = "";
            }
            else
            {
                lblStartDatumValidatie.Text = "Selecteer een start datum.";
            }
        }

        //Validatie om te kijken of de einddatum kalendar is ingevuld.
        protected void calEindDatumValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = calEindDatum.SelectedDate != default(DateTime);

            if (args.IsValid)
            {
                lblEindDatumValidatie.Text = "";
            }
            else
            {
                lblEindDatumValidatie.Text = "Selecteer een eind datum.";
            }
        }

        //Haalt alle gebruikers op met hun aanwezigheid.
        protected void btnGebruikersOphalen_OnClick(object sender, EventArgs e)
        {
            List<string>[] dataTable = _eventBeheerController.GebruikersOphalen();

            ddlAanwezigen.DataSource = dataTable[0];
            ddlAfwezigen.DataSource = dataTable[1];

            ddlAanwezigen.DataBind();
            ddlAfwezigen.DataBind();
        }

        protected void ddlEvenementen_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Kijken of de sessie van de producten nog bestaat.
            if(Session["events"] != null)
            {
                //Het geselcteerde product word gezicht in de lijst.
                _events = Session["events"] as List<Event>;

                Event geselecteerdEvent =
                    _events.Find(selectedEvent => selectedEvent.ID == Convert.ToInt32(ddlEvenementen.SelectedItem.Value));

                //Als er een product is geselecteerd worden de textboxen gevuld met de bijbehorende data.
                if (geselecteerdEvent != null)
                {
                    ddlLocaties.SelectedValue = geselecteerdEvent.Locatie.ID.ToString();

                    tbEvenementNaam.Text = geselecteerdEvent.Naam;
                    calStartDatum.SelectedDate = geselecteerdEvent.DatumStart;
                    calEindDatum.SelectedDate = geselecteerdEvent.DatumEinde;
                    tbMaxAantalBezoekers.Text = geselecteerdEvent.MaxBezoekers.ToString();

                    btnEvenementCreeren.Text = "Aanpassen";
                }
                //als de dropdownlist staat op een nieuw product aanmaken word de data leeg gemaakt.
                else
                {
                    ddlLocaties.SelectedValue = "0";

                    tbEvenementNaam.Text = "";
                    calStartDatum.SelectedDate = DateTime.Now;
                    calEindDatum.SelectedDate = DateTime.Now;
                    tbMaxAantalBezoekers.Text = "";

                    btnEvenementCreeren.Text = "Aanmaken";
                }
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnPlekkenOphalen_OnClick(object sender, EventArgs e)
        {
            if (Session["locaties"] != null)
            {
                List<Plek>[] plekken = _eventBeheerController.PlekkenOphalen(Session["locaties"] as List<Locatie>);

                foreach (Plek plek in plekken[0])
                {
                    ddlVerkrijgbarePlekken.Items.Add(new ListItem(plek.Nummer.ToString(), plek.ID.ToString()));
                }

                foreach (Plek plek in plekken[1])
                {
                    ddlBezettePlekken.Items.Add(new ListItem(plek.Nummer.ToString(), plek.ID.ToString()));
                }

                ddlVerkrijgbarePlekken.DataBind();
                ddlBezettePlekken.DataBind();
            }
            else
            {
                //pagina word ververst als de lijst niet gevonden word.
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}