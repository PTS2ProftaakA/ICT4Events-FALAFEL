using ICT4Events.Models;
using System;
using System.Collections.Generic;

namespace ICT4Events.Controller
{
    public class TijdlijnController
    {
        Database.Database database = new Database.Database();
        
        public TijdlijnController()
        {
            database.Connect();
        }


        //Methoden
        public List<Bestand> HaalOpBestanden(string specificatie)
        {
            //Zoeken naar bestanden met een specificatie
            List<Bestand> bestanden = new List<Bestand>();
            #region Query
            List<string>[] table;
            string query = "";
            if (specificatie == "")
            {
                query = @"SELECT B.""bijdrage_id"", B.""categorie_id"", B.""bestandslocatie"", B.""grootte"", BD.""account_id"", TO_CHAR(BD.""datum"", 'dd-mm-yy HH24:MI:SS') as DATUM FROM BESTAND B, BIJDRAGE BD WHERE B.""bijdrage_id"" = BD.ID AND BD.""soort"" = 'bestand' ORDER BY ""datum""";
            }
            else
            {
                //Geen idee waar ik op moet zoeken, zoek momenteel op de bestandslocatie
                query = String.Format(@"SELECT B.""bijdrage_id"", B.""categorie_id"", B.""bestandslocatie"", B.""grootte"", BD.""account_id"", TO_CHAR(BD.""datum"", 'dd-mm-yy HH24:MI:SS') AS DATUM FROM BESTAND B, BIJDRAGE BD, CATEGORIE C WHERE B.""bijdrage_id"" = BD.ID AND B.""categorie_id"" = C.""bijdrage_id"" AND C.""naam"" LIKE '{0}'", specificatie);
            }

            List<string> kolommen = new List<string>();
            kolommen.Add("BIJDRAGE_ID");
            kolommen.Add("CATEGORIE_ID");
            kolommen.Add("BESTANDSLOCATIE");
            kolommen.Add("GROOTTE");
            kolommen.Add("ACCOUNT_ID");
            kolommen.Add("DATUM");

            table = database.SelectQuery(query, kolommen);
            #endregion

            if (table[0].Count > 1)
            {
                for (int i = 1; i < table[0].Count; i++)
                {
                    int bijdrageId = Convert.ToInt32(table[0][i]);
                    int categorieId = Convert.ToInt32(table[1][i]);
                    string locatie = Convert.ToString(table[2][i]);
                    int grootte = Convert.ToInt32(table[3][i]);
                    int accountId = Convert.ToInt32(table[4][i]);
                    DateTime datum = DateTime.Parse(Convert.ToString(table[5][i]));

                    Account acc = Account.Get(accountId, database);
                    Categorie c = Categorie.Get(categorieId, database);
                    Bestand b = new Bestand(bijdrageId, acc, datum, Bijdrage.BijdrageType.Bestand, c, locatie, grootte);
                    bestanden.Add(b);
                }
            }
            return bestanden;
        }

        public List<Categorie> HaalAlleOpCategorieën()
        {
            //Alle categorieën ophalen
            List<Categorie> categorieLijst = new List<Categorie>();
            List<string>[] table;

            string query = @"SELECT C.""bijdrage_id"", C.""categorie_id"", C.""naam"", B.""account_id"", TO_CHAR(B.""datum"") as DATUM  FROM CATEGORIE C, BIJDRAGE B WHERE C.""bijdrage_id"" = B.ID";
            List<string> kolommen = new List<string>();
            kolommen.Add("BIJDRAGE_ID");
            kolommen.Add("CATEGORIE_ID");
            kolommen.Add("NAAM");
            kolommen.Add("ACCOUNT_ID");
            kolommen.Add("DATUM");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count <= 1) return null;
            
            for (int i = 1; i < table[0].Count; i++)
            {
                Account acc = Account.Get(Convert.ToInt32(table[3][i]), database);
                int bijdrageId = Convert.ToInt32(table[0][i]);
                DateTime dt = DateTime.ParseExact(Convert.ToString(table[4][i]), "dd-mm-yy", null);
                string naam = Convert.ToString(table[2][i]);
                if (table[1][i] == "")
                {
                    Categorie c = new Categorie(bijdrageId, acc, dt, Bijdrage.BijdrageType.Categorie, null, Convert.ToString(table[2][i]));
                    categorieLijst.Add(c);
                }
                else
                {
                    int categorieId = Convert.ToInt32(table[1][i]);
                    Categorie c = new Categorie(bijdrageId, acc, dt, Bijdrage.BijdrageType.Categorie, Categorie.Get(categorieId, database), naam);
                    categorieLijst.Add(c); ;
                }
            }
            return categorieLijst;
        }


        public List<Categorie> GetChildRows(int id)
        {
            List<Categorie> categorieLijst = new List<Categorie>();
            List<string>[] table;

            string query = String.Format(@"SELECT C.""bijdrage_id"", C.""categorie_id"", C.""naam"", B.""account_id"", TO_CHAR(B.""datum"") as DATUM  FROM CATEGORIE C, BIJDRAGE B WHERE C.""bijdrage_id"" = B.ID AND ""categorie_id"" = {0}", id);
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
                        Categorie c = new Categorie(bijdrageId, acc, dt, Bijdrage.BijdrageType.Categorie, null, Convert.ToString(table[2][i]));
                        categorieLijst.Add(c);
                    }
                    else
                    {
                        int categorieId = Convert.ToInt32(table[1][i]);
                        Categorie c = new Categorie(bijdrageId, acc, dt, Bijdrage.BijdrageType.Categorie, Categorie.Get(categorieId, database), naam);
                        categorieLijst.Add(c); ;
                    }
                }
                return categorieLijst;
            }
            return null;
        }

        public List<Account_Bijdrage> HaalOpAccountBijdrageVanBestand(int bijdrage_id)
        {
            //Alle Account_Bijdrage van een bestand ophalen
            List<string>[] table;
            List<Account_Bijdrage> alleAB = new List<Account_Bijdrage>();

            string query = String.Format(@"SELECT ""account_id"", ""bijdrage_id"", ""like"", ""ongewenst"" FROM ACCOUNT_BIJDRAGE AB WHERE ""bijdrage_id"" = {0} AND ""like"" = 1", bijdrage_id);
            List<string> kolommen = new List<string>();
            kolommen.Add("ACCOUNT_ID");
            kolommen.Add("BIJDRAGE_ID");
            kolommen.Add("LIKE");
            kolommen.Add("ONGEWENST");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count > 1)
            {
                for (int i = 1; i < table[0].Count; i++)
                {
                    //Maak een instantie van Account met het id
                    Account acc = Account.Get(Convert.ToInt32(table[0][i]), database);

                    //Krijg hier het bestand met behulp van de Get methode
                    Bestand b = Bestand.Get(Convert.ToInt32(table[1][i]), database);

                    //table[2][i] is 1 of 0 (like)
                    int iLike = Convert.ToInt32(table[2][i]);
                    bool bLike = (iLike != 0);
                    //table[3][i] is 1 of 0 (ongewenst)
                    int iOngewenst = Convert.ToInt32(table[3][i]);
                    bool bOngewenst = (iOngewenst != 0);
                    
                    Account_Bijdrage acc_bijdrage = new Account_Bijdrage(200, acc, b, bLike, bOngewenst);
                    alleAB.Add(acc_bijdrage);
                }
            }

            return alleAB;
        }

        public void MaakBestand(Categorie categorie, string bestandPad)
        {
            List<string>[] table;
            DateTime d = DateTime.Now;
            string str = String.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", d.Day, d.Month, d.Year, d.Hour, d.Minute, d.Second);
            string datum = d.ToString(str);
            //Eerst maak ik een bijdrage, het getal 4 (account_id) moet verandert worden in het account_id in een cookie ofzo.
            database.EditDatabase(String.Format(@"INSERT INTO BIJDRAGE(""account_id"", ""datum"", ""soort"") VALUES( {0} , to_date('{1}', 'DD-MM-YY HH24:MI:SS'), '{2}')", 4, datum, "bestand"));

            List<string> kolom = new List<string>();
            kolom.Add("MAX(ID)");
            table = database.SelectQuery("SELECT MAX(ID) FROM BIJDRAGE", kolom);
            int id = Convert.ToInt32(table[0][1]);

            //Grootte bestand nog niet zeker hoe dat moet gebeuren, voor de rest zou dit een bestand moeten toevoegen
            database.EditDatabase(String.Format("INSERT INTO BESTAND VALUES ({0}, {1}, '{2}', {3})", id, categorie.ID, bestandPad, 500));
        }

        public void MaakCategorie(string naam, Categorie c)
        {
            DateTime d = DateTime.Now;
            string str = String.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", d.Day, d.Month, d.Year, d.Hour, d.Minute, d.Second);
            string datum = d.ToString(str);
            //Eerst maak ik een bijdrage, het getal 1 (account_id) moet verandert worden in het account_id in een cookie ofzo.
            database.EditDatabase(String.Format(@"INSERT INTO BIJDRAGE(""account_id"", ""datum"", ""soort"") VALUES( {0} , to_date('{1}', 'DD-MM-YY HH24:MI:SS'), '{2}')", 4, datum, "categorie"));
            
            List<string>[] table;
            List<string> kolom = new List<string>();

            kolom.Add("MAX(ID)");
            table = database.SelectQuery("SELECT MAX(ID) FROM BIJDRAGE", kolom);
            int id = Convert.ToInt32(table[0][1]);

            database.EditDatabase(String.Format("INSERT INTO CATEGORIE VALUES({0}, {1}, '{2}')", id, c.ID, naam));
            
        }

        //Heb hier een 2de constructor voor gemaakt zonder de categorie, dit is dan dus een main categorie
        public void MaakCategorie(string naam)
        {
            DateTime d = DateTime.Now;
            string str = String.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", d.Day, d.Month, d.Year, d.Hour, d.Minute, d.Second);
            string datum = d.ToString(str);
            //Eerst maak ik een bijdrage, het getal 1 (account_id) moet verandert worden in het account_id in een cookie ofzo.
            database.EditDatabase(String.Format(@"INSERT INTO BIJDRAGE(""account_id"", ""datum"", ""soort"") VALUES( {0} , to_date('{1}', 'DD-MM-YY HH24:MI:SS'), '{2}')", 
                4, datum, "categorie"));

            List<string>[] table;
            List<string> kolom = new List<string>();
            kolom.Add("MAX(ID)");
            table = database.SelectQuery("SELECT MAX(ID) FROM BIJDRAGE", kolom);
            int id = Convert.ToInt32(table[0][1]);

            database.EditDatabase(String.Format(@"INSERT INTO CATEGORIE(""bijdrage_id"", ""naam"") VALUES({0}, '{1}')", id, naam));

        }

        //Zelf bedacht door Tim -- denk dat dit het makkelijkst is
        public int GetLikes(int bijdrage_id)
        {
            List<Account_Bijdrage> aantal = HaalOpAccountBijdrageVanBestand(bijdrage_id);
            return aantal.Count;
        }

        public bool AlreadyExists(int bijdrage_id, int account_id, string condition)
        {
            List<string>[] table;
            string query =
                String.Format(
                    @"SELECT COUNT(*) AS AANTAL FROM ACCOUNT_BIJDRAGE WHERE ""account_id"" = {0} AND ""bijdrage_id"" = {1} AND ""{2}"" = 1",
                    account_id, bijdrage_id, condition);
            List<string> kolommen = new List<string>();
            kolommen.Add("AANTAL");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count > 1)
            {
                if (Convert.ToInt32(table[0][1]) == 0)
                {
                    return false;
                } 
            }
            return true;
        }

        public List<Bericht> Getberichten(int b_id)
        {
            List<Bericht> reacties = new List<Bericht>();
            List<string>[] table;
            string query =
                String.Format(
                    @"SELECT B.""bijdrage_id"", B.""titel"", B.""inhoud"", BD.""account_id"", TO_CHAR(BD.""datum"", 'dd-mm-yy') AS DATUM FROM BERICHT B, BIJDRAGE_BERICHT BB, BIJDRAGE BD WHERE B.""bijdrage_id"" = BB.""bericht_id"" AND B.""bijdrage_id"" = BD.ID AND BB.""bijdrage_id"" = {0} ORDER BY BD.""datum""",
                    b_id);
            List<string> kolommen = new List<string>();
            kolommen.Add("BIJDRAGE_ID");
            kolommen.Add("TITEL");
            kolommen.Add("INHOUD");
            kolommen.Add("ACCOUNT_ID");
            kolommen.Add("DATUM");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count > 1)
            {
                for (int i = 1; i < table[0].Count; i++)
                {
                    int id = Convert.ToInt32(table[0][i]);

                    Bericht b = Bericht.Get(id, database);

                    reacties.Add(b);
                }
                return reacties;
            }
            return null;
        }

        public void MaakBericht(string text, string titel, int b_id)
        {
            DateTime d = DateTime.Now;
            string str = String.Format("{0:00}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", d.Day, d.Month, d.Year, d.Hour, d.Minute, d.Second);
            string datum = d.ToString(str);


            database.EditDatabase(String.Format(@"INSERT INTO BIJDRAGE(""account_id"", ""datum"", ""soort"") VALUES( {0} , to_date('{1}', 'DD-MM-YY HH24:MI:SS'), '{2}')",
                4, datum, "bericht"));
            List<string>[] table;
            List<string> kolom = new List<string>();
            kolom.Add("MAX(ID)");
            table = database.SelectQuery("SELECT MAX(ID) FROM BIJDRAGE", kolom);
            int id = Convert.ToInt32(table[0][1]);

            string query = String.Format(@"INSERT INTO BERICHT(""bijdrage_id"", ""titel"", ""inhoud"") VALUES({0}, '{1}', '{2}')", id, titel, text);
            database.EditDatabase(query);

            query = String.Format(@"INSERT INTO BIJDRAGE_BERICHT(""bijdrage_id"", ""bericht_id"") VALUES({0}, {1})", b_id, id);
            database.EditDatabase(query);

        }
    }
}