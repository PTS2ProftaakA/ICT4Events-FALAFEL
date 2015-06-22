using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Locatie : Database.IDatabase
    {
        //Fields
        private int _id;
        private string _naam;
        private string _straat;
        private int _nr;
        private string _postcode;
        private string _plaats;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; }
        }

        public string Straat
        {
            get { return _straat; }
            set { _straat = value; }
        }

        public int Nr
        {
            get { return _nr; }
            set { _nr = value; }
        }

        public string Postcode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        public string Plaats
        {
            get { return _plaats; }
            set { _plaats = value; }
        }
        #endregion

        //Constructor
        public Locatie(int id, string naam, string straat, int nr, string postcode, string plaats)
        {
            this._id = id;
            this._naam = naam;
            this._straat = straat;
            this._nr = nr;
            this._postcode = postcode;
            this._plaats = plaats;
        }

        //Deze methode voert een database commando uit die een specifieke locatie uit de database haalt
        //Roep deze methode alleen uit wanneer je het ID weet van de locatie die je nodig hebt
        public static Locatie ZoekLocatieMetID(int ID, Database.Database database)
        {
            int testnummer = -1;
            List<string> LocatieKolommen = new List<string>();
            Locatie locatie = null;

            LocatieKolommen.Add("ID");
            LocatieKolommen.Add("naam");
            LocatieKolommen.Add("straat");
            LocatieKolommen.Add("nr");
            LocatieKolommen.Add("postcode");
            LocatieKolommen.Add("plaats");

            List<string>[] dataTable = database.SelectQuery("SELECT * FROM LOCATIE WHERE ID = " + ID, LocatieKolommen);
            if (dataTable[0].Count() > 1)
            {
                for (int i = 1; i < dataTable[0].Count(); i++)
                {
                    if (dataTable[3][i] != "")
                    {
                        testnummer = Convert.ToInt32(dataTable[3][i]);
                    }
                    locatie = new Locatie(
                        Convert.ToInt32(dataTable[0][i]),
                        dataTable[1][i],
                        dataTable[2][i],
                        testnummer,
                        dataTable[4][i],
                        dataTable[5][i]
                        );
                }
            }

            return locatie;
        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {

        }
    }
}