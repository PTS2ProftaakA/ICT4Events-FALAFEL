using System;
using System.Collections.Generic;

namespace ICT4Events.Models
{
    public class Categorie : Bijdrage
    {
        //Fields
        private Categorie _categorie;
        private string _naam;

        //Properties
        #region Properties
        public Categorie SubCategorie
        {
            get { return _categorie; }
            set { _categorie = value; }
        }

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; }
        }
        #endregion

        //Constructor
        public Categorie(int id, Account account, DateTime datum, BijdrageType soort, Categorie categorie, string naam)
            : base(id, account, datum, soort)
        {
            this._categorie = categorie;
            this._naam = naam;
        }

        public void Toevoegen(Database.Database database)
        {

        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {
            string query = String.Format(@"DELETE FROM CATEGORIE WHERE  ""bijdrage_id"" = {0}", ID);
            database.EditDatabase(query);
        }

        public static Categorie Get(int id, Database.Database database)
        {
            List<string>[] table;

            string query = String.Format(@"SELECT C.""bijdrage_id"", C.""categorie_id"", C.""naam"", B.""account_id"", TO_CHAR(B.""datum"") as DATUM  FROM CATEGORIE C, BIJDRAGE B WHERE C.""bijdrage_id"" = B.ID AND C.""bijdrage_id"" = {0}", id);
            List<string> kolommen = new List<string>();
            kolommen.Add("BIJDRAGE_ID");
            kolommen.Add("CATEGORIE_ID");
            kolommen.Add("NAAM");
            kolommen.Add("ACCOUNT_ID");
            kolommen.Add("DATUM");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count > 1)
            {
                for (int i = 1; i < table[0].Count; i++)
                {
                    Account acc = Account.Get(Convert.ToInt32(table[3][i]), database);
                    int bijdrageId = Convert.ToInt32(table[0][i]);
                    DateTime dt = DateTime.ParseExact(Convert.ToString(table[4][i]), "dd-mm-yy", null);
                    string naam = Convert.ToString(table[2][i]);
                    if (table[1][i] == "")
                    {
                        Categorie c = new Categorie(bijdrageId, acc, dt, BijdrageType.Categorie, null, Convert.ToString(table[2][i]));
                        return c;
                    }
                    else
                    {
                        int categorieId = Convert.ToInt32(table[1][i]);
                        Categorie c = new Categorie(bijdrageId, acc, dt, BijdrageType.Categorie, Get(categorieId, database), naam);
                        return c;
                    }
                }
            }

            return null;
        }
    }
}