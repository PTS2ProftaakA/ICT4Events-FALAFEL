using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICT4Events.Models;

namespace ICT4Events.Controllers
{
    public class EventBeheerController
    {
        /// <summary>
        /// Deze klasse regelt al de omzetting van de event gui/logische laag naar de database laag.
        /// Data wordt omgezet naar objecten en andersom.
        /// </summary>
        
        private MasterController _masterController;

        public EventBeheerController()
        {
            _masterController = new MasterController();
        }

        //Het toevoegen van een evenement.
        public void EventToevoegen(int locatieID, string naam, DateTime datumStart, DateTime datumEinde, int maxBezoekers)
        {
            _masterController.EventToevoegen(locatieID, naam, datumStart, datumEinde, maxBezoekers);
        }

        //Het verwijderen van een evenement.
        public void VerwijderEvent(Event verwijderEvent)
        {
            _masterController.VerwijderEvent(verwijderEvent);
        }

        //Het aanpassen van een evenement.
        public void AanpassenEvent(Event aanpasEvent)
        {
            _masterController.AanpassenEvent(aanpasEvent);
        }

        //Het ophalen van alle locaties met gebruik van een procedure.
        public List<Locatie> VerkrijgAlleLocaties()
        {
            List<string>[] dataTable = _masterController.KrijgAlleLocatiesProcedure();
            List<Locatie> locaties = new List<Locatie>();

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                Locatie locatie = new Locatie(Convert.ToInt32(dataTable[0][i]), dataTable[1][i], dataTable[2][i], 2, dataTable[4][i], dataTable[5][i]);
                locaties.Add(locatie);
            }

            return locaties;
        }

        //Het ophalen van alle evenementen met gebruik van een procedure.
        public List<Event> VerkrijgAlleEvents(List<Locatie> locaties)
        {
            List<string>[] dataTable = _masterController.KrijgAlleEventsProcedure();
            List<Event> events = new List<Event>();

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                Locatie foundLocatie = null;

                foreach (Locatie locatie in locaties)
                {
                    if (locatie.ID == Convert.ToInt32(dataTable[1][i]))
                    {
                        foundLocatie = locatie;
                    }
                }
                Event nieuwEvent = new Event(Convert.ToInt32(dataTable[0][i]), foundLocatie, dataTable[2][i],
                Convert.ToDateTime(dataTable[3][i]), Convert.ToDateTime(dataTable[4][i]),
                Convert.ToInt32(dataTable[5][i]));

                events.Add(nieuwEvent);
            }

            return events;
        }

        //Het ophalen van alle aanwezige en niet aanwezige gebruikers.
        public List<string>[] GebruikersOphalen()
        {
            List<string>[] dataTable = _masterController.KrijgAlleGebruikersAanwezigheid();

            List<string>[] gebruikers = new List<string>[2];
            gebruikers[0] = new List<string>();
            gebruikers[1] = new List<string>();

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                if (Convert.ToInt32(dataTable[1][i]) == 1)
                {
                    gebruikers[0].Add(dataTable[0][i]);
                }
                else
                {
                    gebruikers[1].Add(dataTable[0][i]);
                }
            }

            return gebruikers;
        }

        //Het ophalen van alle betalende en niet betalende gebruikers.
        public List<Plek>[] PlekkenOphalen(List<Locatie> locaties)
        {
            List<string>[] dataTable = _masterController.KrijgAlleBeschikbaarheidPlekken();

            List<Plek>[] plekken = new List<Plek>[2];
            plekken[0] = new List<Plek>();
            plekken[1] = new List<Plek>();

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                if (Convert.ToInt32(dataTable[4][i]) == 0)
                {
                    plekken[0].Add(new Plek(Convert.ToInt32(dataTable[0][i]), locaties.Find(locatie => locatie.ID == Convert.ToInt32(dataTable[1][i])), Convert.ToInt32(dataTable[2][i]), Convert.ToInt32(dataTable[3][i]), null));
                }
                else
                {
                    plekken[1].Add(new Plek(Convert.ToInt32(dataTable[0][i]), locaties.Find(locatie => locatie.ID == Convert.ToInt32(dataTable[1][i])), Convert.ToInt32(dataTable[2][i]), Convert.ToInt32(dataTable[3][i]), null));
                }
            }

            return plekken;
        }
    }
}
