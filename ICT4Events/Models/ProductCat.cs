using System;
using System.Collections.Generic;
using System.Linq;

namespace ICT4Events.Models
{
    public class ProductCat : Database.IDatabase
    {
        //Fields
        private int _id;
        private ProductCat _productCat;
        private string _naam;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public ProductCat SubProductCat
        {
            get { return _productCat; }
            set { _productCat = value; }
        }

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; }
        }
        #endregion

        //Constructors
        public ProductCat(int id, ProductCat productCat, string naam)
        {
            this._id = id;
            this._productCat = productCat;
            this._naam = naam;
        }

        public ProductCat(int id, string naam)
        {
            this._id = id;
            this._naam = naam;
        }

        public static ProductCat ZoekSpecifiekeProductCat(Database.Database database, int ID)
        {
            List<string> productcatKolommen = new List<string>();
            ProductCat productcat = null;

            productcatKolommen.Add("ID");
            productcatKolommen.Add("productcat_id");
            productcatKolommen.Add("naam");

            List<string>[] dataTable = database.SelectQuery(@"SELECT * FROM PRODUCTCAT WHERE ID =" + ID, productcatKolommen);

            if (dataTable[0].Count() > 1)
            {
                for (int i = 1; i < dataTable[0].Count(); i++)
                {
                    productcat = new ProductCat(Convert.ToInt32(dataTable[0][i]),null,(string)dataTable[2][i]);
                }
            }
            return productcat;
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