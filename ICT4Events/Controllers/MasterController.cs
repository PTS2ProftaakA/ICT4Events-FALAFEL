using System;
using System.Collections.Generic;
using ICT4Events.Models;
using Oracle.ManagedDataAccess.Client;

namespace ICT4Events.Controllers
{
    public class MasterController
    {
        /// <summary>
        /// Deze klasse is gebruikt voor de verbinding tussen logische en datalaag.
        /// Om te zorgen dat er niet veel verbindingen open zijn worden alle data overdrachten vai deze klasse gedaan.
        /// </summary>
        private Database.Database _database;

        public Database.Database Database
        {
            get { return _database; }
        }

        public MasterController()
        {
            this._database = new Database.Database();

        }

        //Het verkrijgen van alle data over alle locaties met gebruik van een procedure.
        public List<string>[] KrijgAlleLocatiesProcedure()
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("locaties", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("ID");
            columns.Add("naam");
            columns.Add("straat");
            columns.Add("nr");
            columns.Add("postcode");
            columns.Add("plaats");

            List<string>[] dataTable = _database.ExecuteProcedure("verkrijgAlleLocaties", columns, inParameters, outParameters);
            return dataTable;
        }

        //Het verkrijgen van alle data over alle evenementen met gebruik van een procedure.
        public List<string>[] KrijgAlleEventsProcedure()
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("events", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("ID");
            columns.Add("locatie_id");
            columns.Add("naam");
            columns.Add("datumStart");
            columns.Add("datumEinde");
            columns.Add("maxBezoekers");

            List<string>[] dataTable = _database.ExecuteProcedure("verkrijgAlleEvents", columns, inParameters, outParameters);
            return dataTable;
        }

        //Het verkrijgen van alle data over alle producten met gebruik van een procedure.
        public List<string>[] KrijgAlleProductenProcedure()
        {

            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("producten", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("ID");
            columns.Add("productcat_id");
            columns.Add("merk");
            columns.Add("serie");
            columns.Add("typenummer");
            columns.Add("prijs");

            List<string>[] dataTable = _database.ExecuteProcedure("verkrijgAlleProducten", columns, inParameters, outParameters);
            return dataTable;
        }

        //Het verkrijgen van alle data over alle categorien van producten met gebruik van een procedure.
        public List<string>[] KrijgAlleProductCatProcedure()
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("productCat", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("ID");
            columns.Add("productcat_id");
            columns.Add("naam");

            List<string>[] dataTable = _database.ExecuteProcedure("verkrijgAlleProductCat", columns, inParameters, outParameters);
            return dataTable;
        }

        //Het verkrijgen van alle data over alle productexemplaren met gebruik van een procedure.
        public List<string>[] KrijgAlleProductExemplaren()
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("exemplaren", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("ID");
            columns.Add("product_id");
            columns.Add("volgnummer");
            columns.Add("barcode");

            List<string>[] dataTable = _database.ExecuteProcedure("verkrijgAlleProdExemplaren", columns, inParameters, outParameters);
            return dataTable;
        }

        //Het verkrijgen van alle data over alle verhuringen met gebruik van een procedure.
        public List<string>[] KrijgAlleHuidigeVerhuringen()
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("verhuringen", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("ID");
            columns.Add("productexemplaar_id");
            columns.Add("res_pb_id");
            columns.Add("datumIn");
            columns.Add("datumUit");
            columns.Add("prijs");
            columns.Add("betaald");

            List<string>[] dataTable = _database.ExecuteProcedure("verkrijgHuidigeVerhuur", columns, inParameters, outParameters);
            return dataTable;
        }

        //Het verkrijgen van de aanwezigheid van alle gebruikers met gebruik van een procedure.
        public List<string>[] KrijgAlleGebruikersAanwezigheid()
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("aanwezigen", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("gebruikersnaam");
            columns.Add("aanwezig");

            List<string>[] dataTable = _database.ExecuteProcedure("lijstAanwezige", columns, inParameters, outParameters);
            return dataTable;
        }

        //Het verwijderen van een account.
        //Als het niet lukt word er een specifieke error meegegeven door middel van een procedure.
        public string VerwijderAccount(int accountID)
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            inParameters.Add(new OracleParameter("account_id", accountID)); 
            
            List<OracleParameter> outParameters = new List<OracleParameter>();

            List<string> columns = new List<string>();

            try
            {
                _database.ExecuteProcedure("accountVerwijderen", columns, inParameters, outParameters);
                return "";
            }
            catch(Exception error)
            {
                return error.Message;
            }
        }

        //Het verwijderen van een evenement.
        public void VerwijderEvent(Event verwijderEvent)
        {
            verwijderEvent.Verwijderen(_database);
        }

        //Het aanpassen van een evenement.
        public void AanpassenEvent(Event aanpasEvent)
        {
            aanpasEvent.Aanpassen(_database);
        }

        //Het toevoegen van een evenement.
        public void EventToevoegen(int locatieID, string naam, DateTime datumStart, DateTime datumEinde, int maxBezoekers)
        {
            _database.EditDatabase(String.Format("INSERT INTO EVENT VALUES(NULL, {0}, '{1}', to_date('{2}', 'dd/mm/yyyy HH24:MI:SS'), to_date('{3}', 'dd/mm/yyyy HH24:MI:SS'), {4})",
                locatieID, naam, datumStart, datumEinde, maxBezoekers));
        }

        //Het verwijderen van een product.
        public void VerwijderProduct(Product verwijderProduct)
        {
            verwijderProduct.Verwijderen(_database);
        }

        //Het aanpassen van een product.
        public void AanpassenProduct(Product verwijderProduct)
        {
            verwijderProduct.Aanpassen(_database);
        }

        //Het toevoegen van een product.
        public void ToevoegenProduct(int productcatID, string merk, string serie, int typenummer, int prijs)
        {
            _database.EditDatabase(String.Format("INSERT INTO PRODUCT VALUES(NULL, {0}, '{1}', '{2}', {3}, {4})",
                productcatID, merk, serie, typenummer, prijs));
        }

        //Het verwijderen van een productexemplaar.
        public void VerwijderProductExemplaar(ProductExemplaar verwijderProductExemplaar)
        {
            verwijderProductExemplaar.Verwijderen(_database);
        }

        //Het toevoegen van een productexemplaar.
        public void ToevoegenProductExemplaar(int productID)
        {
            _database.EditDatabase(String.Format(@"INSERT INTO PRODUCTEXEMPLAAR VALUES(NULL, {0}, (SELECT MAX(""volgnummer"") FROM PRODUCTEXEMPLAAR WHERE ""product_id"" = {0}) + 1, to_char(1000 + {0} - 1) || '.' || LPAD((SELECT MAX(""volgnummer"") FROM PRODUCTEXEMPLAAR WHERE ""product_id"" = {0}) + 1, 3, '0'))",
                productID));
        }

        //Het verwijderen van een huring.
        public void VerwijderVerhuring(Verhuur verwijderVerhuring)
        {
            verwijderVerhuring.Verwijderen(_database);
        }

        //Het activeren van een account door middel van een procedure.
        public void ActiveerAccount(string activeercode)
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            inParameters.Add(new OracleParameter("activeerCode", activeercode)); 
            
            List<OracleParameter> outParameters = new List<OracleParameter>();

            List<string> columns = new List<string>();

            _database.ExecuteProcedure("activeerAccount", columns, inParameters, outParameters);
        }

        //De betalingsstatus bekijken van de reservering van een account via barcode.
        //Dit gebeurt d.m.v. een stored procedure.
        public bool BetalingsCheck(string barcode)
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            inParameters.Add(new OracleParameter("barcodeIN", barcode));

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("betaaldOUT", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("betaald");

            List<string>[] dataTable = _database.ExecuteProcedure("betalingsCheck", columns, inParameters, outParameters);

            return Convert.ToInt32(dataTable[0][0]) == 1;
        }

        //Deze methode leegt alle data uit de database.
        //Te gebruiken na een evenement.
        public void DatabaseLegen()
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            List<string> columns = new List<string>();

            List<string>[] dataTable = _database.ExecuteProcedure("databaseLegen", columns, inParameters, outParameters);
        }
    }
}