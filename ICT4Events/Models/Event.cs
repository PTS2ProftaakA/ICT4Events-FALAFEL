using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Event : Database.IDatabase
    {
        //Fields
        private int _id;
        private Locatie _locatie;
        private string _naam;
        private DateTime _datumStart;
        private DateTime _datumEinde;
        private int _maxBezoekers;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Locatie Locatie
        {
            get { return _locatie; }
            set { _locatie = value; }
        }

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; }
        }

        public DateTime DatumStart
        {
            get { return _datumStart; }
            set { _datumStart = value; }
        }

        public DateTime DatumEinde
        {
            get { return _datumEinde; }
            set { _datumEinde = value; }
        }

        public int MaxBezoekers
        {
            get { return _maxBezoekers; }
            set { _maxBezoekers = value; }
        }
        #endregion

        //Constructor
        public Event(int id, Locatie locatie, string naam, DateTime datumStart, DateTime datumEinde, int maxBezoekers)
        {
            this._id = id;
            this._locatie = locatie;
            this._naam = naam;
            this._datumStart = datumStart;
            this._datumEinde = datumEinde;
            this._maxBezoekers = maxBezoekers;
        }

        //Deze methode voert een database commando uit die een specifiek event uit de database haalt
        //Roep deze methode alleen uit wanneer je het ID weet van het event die je nodig hebt
        public static Event ZoekEventMetID(Database.Database database, int ID)
        {
            //Maak een lijst aan van alle kolommen die voorkomen in de database
            List<string> eventKolommen = new List<string>();
            //Maak een Event aan zodat je deze terug kan geven wanneer deze gevuld is
            Event _event = null;

            //Dit zijn alle Kolommen in de tabel "Event"
            eventKolommen.Add("ID");
            eventKolommen.Add("LOCATIE_ID");
            eventKolommen.Add("NAAM");
            eventKolommen.Add("DATUMSTART");
            eventKolommen.Add("DATUMEINDE");
            eventKolommen.Add("MAXBEZOEKERS");

            //Voer een select query uit op de database met de kolommen uit de tabel
            //Datatable = een manier van resultaten uit een quert op te slaan
            List<string>[] dataTable = database.SelectQuery(@"SELECT * FROM EVENT WHERE ID = " + ID, eventKolommen);

            //Begin het vullen van de database alleen wanneer er gegevens gevonden zijn in de dataTable
            if (dataTable[0].Count() > 1)
            {
                //Voer een loop uit voor elke rij in de dataTable
                for (int i = 1; i < dataTable[0].Count(); i++)
                {
                    //Maak een nieuw event voor elk gevonden record in de database
                    _event = new Event(
                        Convert.ToInt32(dataTable[0][i]),
                        Locatie.ZoekLocatieMetID(Convert.ToInt32(dataTable[1][i]),database),
                        (string)dataTable[2][i],
                        DateTime.Parse((string)dataTable[3][i]), 
                        DateTime.Parse((string)dataTable[4][i]),
                        Convert.ToInt32(dataTable[5][i]));
                }
            }
            //Geef het Event terug
            return _event;
        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {

        }
    }
}