using System;
using System.Collections.Generic;

namespace ICT4Events.Models
{
    public class Bestand : Bijdrage
    {
        //Fields
        private Categorie _categorie;
        private string _bestandsLocatie;
        private int _grootte;

        //Properties
        #region Properties
        public Categorie Categorie
        {
            get { return _categorie; }
            set { _categorie = value; }
        }

        public string BestandsLocatie
        {
            get { return _bestandsLocatie; }
            set { _bestandsLocatie = value; }
        }

        public int Grootte
        {
            get { return _grootte; }
            set { _grootte = value; }
        }
        #endregion

        //Constructor
        public Bestand(int id, Account account, DateTime datum, BijdrageType soort, Categorie categorie, string bestandslocatie, int grootte)
            : base(id, account, datum, soort)
        {
            this._categorie = categorie;
            this._bestandsLocatie = bestandslocatie;
            this._grootte = grootte;
        }

        public void Toevoegen(Database.Database database)
        {

        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {
            //List<int> berichten = new List<int>();
            //string query = String.Format(@"SELECT ""bericht_id"" FROM BIJDRAGE_BERICHT WHERE ""bijdrage_id"" = {0}", ID);
            //List<string>[] table;
            //List<string> kolommen = new List<string>();
            //kolommen.Add("BERICHT_ID");

            //table = database.SelectQuery(query, kolommen);

            //if (table[0].Count > 1)
            //{
            //    for (int i = 1; i < table[0].Count; i++)
            //    {
            //        int nr = Convert.ToInt32(table[0][i]);
            //        berichten.Add(nr);
            //    }
            //}

            //query = 
            //    String.Format(
            //        @"DELETE FROM ACCOUNT_BIJDRAGE WHERE ""bijdrage_id"" = {0}", ID);
            //database.EditDatabase(query);

            //query =
            //    String.Format(
            //        @"DELETE FROM ACCOUNT_BIJDRAGE AB WHERE AB.""bijdrage_id"" IN (SELECT BB.""bericht_id"" FROM BIJDRAGE_BERICHT BB WHERE BB.""bijdrage_id"" = {0})", ID);
            //database.EditDatabase(query);

            //query =
            //    String.Format(
            //        @"DELETE FROM BIJDRAGE_BERICHT WHERE ""bijdrage_id"" = {0}", ID);
            //database.EditDatabase(query);

            //foreach (int i in berichten)
            //{
            //    query =
            //    String.Format(
            //        @"DELETE FROM BERICHT B WHERE B.""bijdrage_id"" = {0}", i);
            //    database.EditDatabase(query);

            //    query =
            //    String.Format(
            //        @"DELETE FROM BIJDRAGE B WHERE B.ID = {0}", i);
            //    database.EditDatabase(query);
            //}

            //query = 
            //    String.Format(
            //        @"DELETE FROM BESTAND WHERE ""bijdrage_id"" = {0}", ID);
            //database.EditDatabase(query);

            //query = 
            //    String.Format(
            //        @"DELETE FROM BIJDRAGE WHERE ID = {0}", ID);
            //database.EditDatabase(query);
            string query = String.Format(@"DELETE FROM BESTAND WHERE ""bijdrage_id"" = {0}", ID);
            database.EditDatabase(query);
        }

        public static Bestand Get(int id, Database.Database database)
        {
            List<string>[] table;
            string query = String.Format(@"SELECT B.""bijdrage_id"", B.""categorie_id"", B.""bestandslocatie"", B.""grootte"", BD.""account_id"", TO_CHAR(BD.""datum"", 'dd-mm-yy HH24:MI:SS') as DATUM FROM BESTAND B, BIJDRAGE BD WHERE B.""bijdrage_id"" = BD.ID AND BD.""soort"" = 'bestand' AND BD.ID = {0}", id);

            List<string> kolommen = new List<string>();
            kolommen.Add("BIJDRAGE_ID");
            kolommen.Add("CATEGORIE_ID");
            kolommen.Add("BESTANDSLOCATIE");
            kolommen.Add("GROOTTE");
            kolommen.Add("ACCOUNT_ID");
            kolommen.Add("DATUM");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count > 1)
            {
                int bijdrageId = Convert.ToInt32(table[0][1]);
                int categorieId = Convert.ToInt32(table[1][1]);
                string locatie = Convert.ToString(table[2][1]);
                int grootte = Convert.ToInt32(table[3][1]);
                int accountId = Convert.ToInt32(table[4][1]);
                DateTime dt = DateTime.Parse(Convert.ToString(table[5][1]));

                Account acc = Account.Get(accountId, database);
                Categorie c = Categorie.Get(categorieId, database);
                Bestand b = new Bestand(bijdrageId, acc, dt, Bijdrage.BijdrageType.Bestand, c, locatie, grootte);
                return b;
            }

            return null;
        }
    }
}