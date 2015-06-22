using System;
using System.Collections.Generic;
using System.Linq;

namespace ICT4Events.Models
{
    public class Account : Database.IDatabase
    {
        //Fields
        private int _id;
        private string _gebruikersnaam;
        private string _email;
        private string _activatieHash;
        private bool _geactiveerd;
        private List<Bijdrage> _bijdrages;


        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Gebruikersnaam
        {
            get { return _gebruikersnaam; }
            set { _gebruikersnaam = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Activatiehash
        {
            get { return _activatieHash; }
            set { _activatieHash = value; }
        }

        public bool Geactiveerd
        {
            get { return _geactiveerd; }
            set { _geactiveerd = value; }
        }

        public List<Bijdrage> Bijdrages
        {
            get { return _bijdrages; }
            set { _bijdrages = value; }
        }
        #endregion

        //Constructor
        public Account(int id, string gebruikersnaam, string email, string activatiehash, bool geactiveerd)
        {
            this._id = id;
            this._gebruikersnaam = gebruikersnaam;
            this._email = email;
            this._activatieHash = activatiehash;
            this._geactiveerd = geactiveerd;
        }
        
        //Deze methode voert een database commando uit die alle gebruikers uit de database haalt
        //Roep deze methode alleen uit wanneer je zeker weet dat je alle gebruikers nodig hebt
        public static List<Account> ZoekAlleGebruikers(Database.Database database)
        {
            //Maak een lijst aan van alle kolommen die voorkomen in de database
            List<string> gebruikerKolommen = new List<string>();
            //Maak een lijst aan van alle accounts zodat je deze terug kan geven wanneer deze gevuld is
            List<Account> accounts = new List<Account>();

            //Dit zijn alle Kolommen in de tabel "Account"
            gebruikerKolommen.Add("ID");
            gebruikerKolommen.Add("GEBRUIKERSNAAM");
            gebruikerKolommen.Add("EMAIL");
            gebruikerKolommen.Add("ACTIVATIEHASH");
            gebruikerKolommen.Add("GEACTIVEERD");

            //Voer een select query uit op de database met de kolommen uit de tabel
            //Datatable = een manier van resultaten uit een quert op te slaan
            List<string>[] dataTable = database.SelectQuery("SELECT * FROM ACCOUNT", gebruikerKolommen);
            
            //Begin het vullen van de database alleen wanneer er gegevens gevonden zijn in de dataTable
            if (dataTable[0].Count() > 1)
            {
                //Voer een loop uit voor elke rij in de dataTable
                for (int i = 1; i < dataTable[0].Count(); i++)
                {
                    //Maak vooraf een boolean (true/false) die kijkt of een booleanwaarde uit de databaase(0/1) waar is of niet
                    //Zet de boolean op waar wanneer het resultaat 1 oplevert
                    bool geactiveerd = Convert.ToInt32(dataTable[4][i]) == 1;

                    //Maak een nieuw account voor elk gevonden record in de database
                    accounts.Add(new Account(
                        Convert.ToInt32(dataTable[0][i]),
                        (string)dataTable[1][i],
                        (string)dataTable[2][i],
                        (string)dataTable[3][i],
                        geactiveerd));
                }
            }
            //Geef de lijst van accounts terug
            return accounts;
        }

        public static Account Get(string username, Database.Database database)
        {
            List<string>[] table;
            string query = String.Format(@"SELECT ID, ""gebruikersnaam"", ""email"", ""activatiehash"", ""geactiveerd"" FROM ACCOUNT WHERE ""gebruikersnaam"" = '{0}'", username);
            List<string> kolommen = new List<string>();
            kolommen.Add("ID");
            kolommen.Add("GEBRUIKERSNAAM");
            kolommen.Add("EMAIL");
            kolommen.Add("ACTIVATIEHASH");
            kolommen.Add("GEACTIVEERD");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count > 1)
            {
                for (int i = 1; i < table[0].Count; i++)
                {
                    bool actief = true;
                    if (table[4][i] != null)
                    {
                        int test;
                        if (int.TryParse(Convert.ToString(table[4][i]), out test)) ;
                        {
                            if (test != 1)
                            {
                                actief = false;
                            }
                        }

                    }
                    Account acc = new Account(Convert.ToInt32(table[0][i]), Convert.ToString(table[1][i]), Convert.ToString(table[2][i]), Convert.ToString(table[3][i]), actief);
                    return acc;
                }
            }

            return null;
        }

        public static Account Get(int id, Database.Database database)
        {
            List<string>[] table;
            string query = String.Format(@"SELECT ID, ""gebruikersnaam"", ""email"", ""activatiehash"", ""geactiveerd"" FROM ACCOUNT WHERE ID = {0}", id);
            List<string> kolommen = new List<string>();
            kolommen.Add("ID");
            kolommen.Add("GEBRUIKERSNAAM");
            kolommen.Add("EMAIL");
            kolommen.Add("ACTIVATIEHASH");
            kolommen.Add("GEACTIVEERD");

            table = database.SelectQuery(query, kolommen);

            if (table[0].Count > 1)
            {
                for (int i = 1; i < table[0].Count; i++)
                {
                    bool actief = true;
                    if (table[4][i] != null)
                    {
                        int test;
                        if (int.TryParse(Convert.ToString(table[4][i]), out test)) ;
                        {
                            if (test != 1)
                            {
                                actief = false;
                            }
                        }

                    }
                    Account acc = new Account(Convert.ToInt32(table[0][i]), Convert.ToString(table[1][i]), Convert.ToString(table[2][i]), Convert.ToString(table[3][i]), actief);
                    return acc;
                }
            }

            return null;
        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {

        }
    }
}