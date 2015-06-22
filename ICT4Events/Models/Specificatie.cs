using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICT4Events.Database;

namespace ICT4Events.Models
{
    public class Specificatie : IDatabase
    {
        //Fields
        private int _id;
        private string _naam;
        private string _waarde;

        //Properties
        #region properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; }
        }

        public string Waarde
        {
            get { return _waarde; }
            set { _waarde = value; }
        }
        #endregion

        //Constructor
        public Specificatie(int id,string naam, string waarde)
        {
            this._id = id;
            this._naam = naam;
            this._waarde = waarde;
        }

        public static List<Specificatie> GeefSpecificatiesVanplek(Database.Database database, int plekID)
        {
            List<string> specificatieKolommen = new List<string>();
            List<Specificatie> specificatieLijst = new List<Specificatie>();

            specificatieKolommen.Add("ID");
            specificatieKolommen.Add("naam");
            specificatieKolommen.Add("waarde");

            List<string>[] dataTable = database.SelectQuery(@"SELECT S.""ID"", S.""naam"", PS.""waarde"" FROM SPECIFICATIE S, PLEK_SPECIFICATIE PS WHERE S.ID = PS.""specificatie_id"" AND PS.""plek_id"" = "+ plekID, specificatieKolommen);

            if (dataTable[0].Count() > 1)
            {
                for (int i = 1; i < dataTable[0].Count(); i++)
                {
                    specificatieLijst.Add(new Specificatie(
                        Convert.ToInt32(dataTable[0][i]),
                        (string)dataTable[1][i],
                        (string)dataTable[2][i]
                        ));
                }
            }

            return specificatieLijst;
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
    }
}