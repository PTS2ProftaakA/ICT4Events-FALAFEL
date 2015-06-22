using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICT4Events.Models;
using Oracle.ManagedDataAccess.Client;

namespace ICT4Events.Conrtoller
{
    public class ReserveringController
    {
        private Database.Database database;
        public ReserveringController()
        {
            database = new Database.Database();

        }

        public void VoegPersoonEnReserveringToe(Persoon persoon, int EventID)
        {
            Event _event = Event.ZoekEventMetID(database, EventID);
            
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            List<string> Columns = new List<string>();

            inParameters.Add(new OracleParameter("p_voornaam", persoon.Voornaam));
            inParameters.Add(new OracleParameter("p_tussenvoegsel", persoon.Tussenvoegsel));
            inParameters.Add(new OracleParameter("p_achternaam", persoon.Achternaam));
            inParameters.Add(new OracleParameter("p_straatnaam", persoon.Straat));
            inParameters.Add(new OracleParameter("p_huisnr", persoon.Huisnr));
            inParameters.Add(new OracleParameter("p_woonplaats", persoon.Woonplaats));
            inParameters.Add(new OracleParameter("p_banknummer", persoon.Banknr));
            inParameters.Add(new OracleParameter("p_reserveringDatumStart", _event.DatumStart));
            inParameters.Add(new OracleParameter("p_reserveringDatumEinde", _event.DatumEinde));

            var dataTable = database.ExecuteProcedure("persoonReserveringToevoegen", Columns, inParameters, outParameters);
        }

        public void VoegAccountsToeAanReservering(Account account)
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            List<string> Columns = new List<string>();

            inParameters.Add(new OracleParameter("p_gebruikersnaam",account.Gebruikersnaam));
            inParameters.Add(new OracleParameter("p_email", account.Email));
            inParameters.Add(new OracleParameter("p_activatiehash", account.Activatiehash));

            var dataTable = database.ExecuteProcedure("voegAccountToeAanReservering", Columns, inParameters, outParameters);
        }

        public void KoppelReserveringAanPlaatsen(int PlekID)
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            List<string> Columns = new List<string>();

            inParameters.Add(new OracleParameter("p_PlekID", PlekID));

            var dataTable = database.ExecuteProcedure("voegPlaatsenToeAanReservering", Columns, inParameters, outParameters);
        }

        private List<string>[] KrijgAlleProductExemplaren()
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            outParameters.Add(new OracleParameter("exemplaren", OracleDbType.RefCursor));

            List<string> columns = new List<string>();
            columns.Add("ID");
            columns.Add("product_id");
            columns.Add("volgnummer");
            columns.Add("barcode");

            List<string>[] dataTable = database.ExecuteProcedure("verkrijgAlleProdExemplaren", columns, inParameters, outParameters);
            return dataTable;
        }

        //Een procedure die alle productenexemplaren ophaald uit de database.
        public List<ProductExemplaar> KrijgAlleProductExemplaren(List<Product> producten)
        {
            List<string>[] dataTable = KrijgAlleProductExemplaren();
            List<ProductExemplaar> productExemplaren = new List<ProductExemplaar>();

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                productExemplaren.Add(new ProductExemplaar(Convert.ToInt32(dataTable[0][i]), producten.Find(product => product.ID == Convert.ToInt32(dataTable[1][i])), Convert.ToInt32(dataTable[2][i]), dataTable[3][i]));
            }

            return productExemplaren;
        }

        public void ReserveerMateriaal(ProductExemplaar exemplaar, Event _event)
        {
            List<OracleParameter> inParameters = new List<OracleParameter>();

            List<OracleParameter> outParameters = new List<OracleParameter>();

            List<string> Columns = new List<string>();

            inParameters.Add(new OracleParameter("p_exemplaarID", exemplaar.ID));
            inParameters.Add(new OracleParameter("p_datumIn", _event.DatumStart));
            inParameters.Add(new OracleParameter("p_datumUit", _event.DatumEinde));
            inParameters.Add(new OracleParameter("p_prijs", exemplaar.Product.Prijs));

            var dataTable = database.ExecuteProcedure("huurMateriaal", Columns, inParameters, outParameters);
        }
    }
}