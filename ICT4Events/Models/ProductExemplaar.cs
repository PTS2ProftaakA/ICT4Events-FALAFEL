using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class ProductExemplaar : Database.IDatabase
    {
        //Fields
        private int _id;
        private Product _product;
        private int _volgnummer;
        private string _barcode;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public int Volgnummer
        {
            get { return _volgnummer; }
            set { _volgnummer = value; }
        }

        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }
        #endregion

        //Constructors
        public ProductExemplaar(int id, Product product, int volgnummer, string barcode)
        {
            this._id = id;
            this._product = product;
            this._volgnummer = volgnummer;
            this._barcode = barcode;
        }

        public void Toevoegen(Database.Database database)
        {

        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {
            database.EditDatabase(String.Format("DELETE FROM PRODUCTEXEMPLAAR WHERE ID = {0}", _id));
        }
    }
}