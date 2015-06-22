using System;
using System.Collections.Generic;

namespace ICT4Events.Models
{
    public class Bericht : Bijdrage
    {
        //Fields
        private string _titel;
        private string _inhoud;
        private Bijdrage _bijdrage;

        //Properties
        #region properties
        public string Titel
        {
            get { return _titel; }
            set { _titel = value; }
        }

        public string Inhoud
        {
            get { return _inhoud; }
            set { _inhoud = value; }
        }

        public Bijdrage Bijdrage
        {
            get { return _bijdrage; }
            set { _bijdrage = value; }
        }
        #endregion

        //Constructor
        public Bericht(int id, Account account, DateTime datum, BijdrageType soort, string titel, string inhoud, Bijdrage bijdrage)
            : base(id, account, datum, soort)
        {
            this._titel = titel;
            this._inhoud = inhoud;
            this._bijdrage = bijdrage;
        }

        public void Toevoegen(Database.Database database)
        {

        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {
            //Hier nog mee on delete cascade kijken of dat mag, anders veel te moeilijk
            string query = String.Format(@"DELETE FROM BERICHT WHERE ""bijdrage_id"" = {0}", ID);
            database.EditDatabase(query);
        }

        public static Bericht Get(int id, Database.Database database)
        {
            List<string>[] table;
            string query = String.Format(
                @"SELECT B.""bijdrage_id"", B.""titel"", B.""inhoud"", BD.""account_id"", TO_CHAR(BD.""datum"", 'dd-mm-yy HH24:MI:SS') as ""DATUM"", BB.""bijdrage_id"" AS ""BSUB"" FROM BERICHT B, BIJDRAGE BD, BIJDRAGE_BERICHT BB WHERE B.""bijdrage_id"" = BD.ID AND B.""bijdrage_id"" = BB.""bericht_id"" AND BD.ID = {0}", id);

            List<string> kolommen = new List<string>();
            kolommen.Add("BIJDRAGE_ID");
            kolommen.Add("TITEL");
            kolommen.Add("INHOUD");
            kolommen.Add("ACCOUNT_ID");
            kolommen.Add("DATUM");
            kolommen.Add("BSUB");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count > 1)
            {
                int bijdrageId = Convert.ToInt32(table[0][1]);
                string titel = Convert.ToString(table[1][1]);
                string inhoud = Convert.ToString(table[2][1]);
                int accountId = Convert.ToInt32(table[3][1]);
                DateTime dt = DateTime.Parse(Convert.ToString(table[4][1]));
                int overId = Convert.ToInt32(table[5][1]);

                Account acc = Account.Get(accountId, database);

                Bijdrage b_sub = GetSub(overId, database);


                Bericht b = new Bericht(bijdrageId, acc, dt, BijdrageType.Bericht, titel, inhoud, b_sub);
                return b;
            }

            return null;
        }

        public static Bijdrage GetSub(int overId, Database.Database database)
        {
            List<string>[] table;
            string query = String.Format(@"SELECT ID, ""soort"" FROM BIJDRAGE WHERE ID = {0}", overId);
            List<string> kolommen = new List<string>();
            kolommen.Add("ID");
            kolommen.Add("SOORT");
            table = database.SelectQuery(query, kolommen);
            if (table[0].Count > 1)
            {
                int id = Convert.ToInt32(table[0][1]);
                string soort = Convert.ToString(table[1][1]);
                if (soort == "bericht")
                {
                    Bericht b = Get(id, database);
                    return b;
                }
                else if (soort == "bestand")
                {
                    Bestand b = Bestand.Get(id, database);
                    return b;
                }
                else if (soort == "categorie")
                {
                    Categorie c = Categorie.Get(id, database);
                    return c;
                }
            }
            return null;
        }
    }
}