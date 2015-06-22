using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Plek : Database.IDatabase
    {
        //Fields
        private int _id;
        private Locatie _locatie;
        private int _nummer;
        private int _capaciteit;
        private List<Specificatie> _specificaties;

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

        public int Nummer
        {
            get { return _nummer; }
            set { _nummer = value; }
        }

        public int Capaciteit
        {
            get { return _capaciteit; }
            set { _capaciteit = value; }
        }

        public List<Specificatie> Specificaties
        {
            get { return _specificaties; }
            set { _specificaties = value; }
        }
        #endregion

        //Constructors
        public Plek(int id, Locatie locatie, int nummer, int capaciteit, List<Specificatie> specificaties)
        {
            this._id = id;
            this._locatie = locatie;
            this._nummer = nummer;
            this._capaciteit = capaciteit;
            this._specificaties = specificaties;
        }

        public static List<Plek> GeefBeschikbarePlekken( Database.Database database)
        {
            
            List<string> plekKolommen = new List<string>();
            List<Plek> plekkenLijst = new List<Plek>();

            plekKolommen.Add("ID");
            plekKolommen.Add("locatie_ID");
            plekKolommen.Add("nummer");
            plekKolommen.Add("capaciteit");

            List<string>[] dataTable = database.SelectQuery(@"SELECT * FROM PLEK WHERE ID NOT IN (SELECT ""plek_id"" FROM PLEK_RESERVERING)", plekKolommen);

            if(dataTable[0].Count() > 1)
            {
                for(int i = 1; i < dataTable[0].Count(); i++)
                {
                    plekkenLijst.Add(new Plek(
                        Convert.ToInt32(dataTable[0][i]),
                        Locatie.ZoekLocatieMetID(Convert.ToInt32(dataTable[1][i]), database),
                        Convert.ToInt32(dataTable[2][i]),
                        Convert.ToInt32(dataTable[3][i]),
                        Specificatie.GeefSpecificatiesVanplek(database,Convert.ToInt32(dataTable[0][i]))
                        ));
                }
            }

            return plekkenLijst;
        }

        public static Plek ZoekPlekMetID(Database.Database database, int ID)
        {
            List<string> plekKolommen = new List<string>();
            Plek plek = null;

            plekKolommen.Add("ID");
            plekKolommen.Add("locatie_ID");
            plekKolommen.Add("nummer");
            plekKolommen.Add("capaciteit");

            List<string>[] dataTable = database.SelectQuery(@"SELECT * FROM PLEK WHERE ID =" + ID, plekKolommen);

            if (dataTable[0].Count() > 1)
            {
                for (int i = 1; i < dataTable[0].Count(); i++)
                {
                    plek = new Plek(
                        Convert.ToInt32(dataTable[0][i]),
                        Locatie.ZoekLocatieMetID(Convert.ToInt32(dataTable[1][i]), database),
                        Convert.ToInt32(dataTable[2][i]),
                        Convert.ToInt32(dataTable[3][i]),
                        Specificatie.GeefSpecificatiesVanplek(database, Convert.ToInt32(dataTable[0][i]))
                        );
                }
            }
            return plek;
        }

        public void Toevoegen(Database.Database database)
        {

        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {

        }

        public override string ToString()
        {
            return "Plaats " + _nummer;
        }
    }
}