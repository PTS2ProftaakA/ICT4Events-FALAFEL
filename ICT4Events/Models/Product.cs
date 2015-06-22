using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Product : Database.IDatabase
    {
        //Fields
        private int _id;
        private ProductCat _productCategorie;
        private string _merk;
        private string _serie;
        private int _typenummer;
        private int _prijs;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public ProductCat ProductCategorie
        {
            get { return _productCategorie; }
            set { _productCategorie = value; }
        }

        public string Merk
        {
            get { return _merk; }
            set { _merk = value; }
        }

        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        public int Typenummer
        {
            get { return _typenummer; }
            set { _typenummer = value; }
        }

        public int Prijs
        {
            get { return _prijs; }
            set { _prijs = value; }
        }
        #endregion

        //Constructors
        public Product(int id, ProductCat productCategorie, string merk, string serie, int typenummer, int prijs)
        {
            this._id = id;
            this._productCategorie = productCategorie;
            this._merk = merk;
            this._serie = serie;
            this._typenummer = typenummer;
            this._prijs = prijs;
        }

        public static List<Product> ZoekAlleProducten(Database.Database database)
        {
            List<string> productKolommen = new List<string>();
            List<Product> productLijst = new List<Product>();

            productKolommen.Add("ID");
            productKolommen.Add("productcat_id");
            productKolommen.Add("merk");
            productKolommen.Add("serie");
            productKolommen.Add("typenummer");
            productKolommen.Add("prijs");

            List<string>[] dataTable = database.SelectQuery(@"SELECT * FROM PRODUCT ORDER BY ID", productKolommen);

            if (dataTable[0].Count() > 1)
            {
                for (int i = 1; i < dataTable[0].Count(); i++)
                {
                    productLijst.Add(new Product(
                        Convert.ToInt32(dataTable[0][i]),
                        ProductCat.ZoekSpecifiekeProductCat(database,Convert.ToInt32(dataTable[1][i])),
                        (string)dataTable[2][i],
                        (string)dataTable[3][i],
                        Convert.ToInt32(dataTable[4][i]),
                        Convert.ToInt32(dataTable[5][i])
                        ));
                }
            }

            return productLijst;
        }

        public void Toevoegen(Database.Database database)
        {

        }

        public void Aanpassen(Database.Database database)
        {
            database.EditDatabase(String.Format(@"UPDATE PRODUCT SET ""productcat_id"" = {0}, ""merk"" = '{1}', ""serie"" = '{2}', ""typenummer"" = {3}, ""prijs"" = {4} WHERE ID = {5}",
                _productCategorie.ID, _merk, _serie, _typenummer, _prijs, _id));
        }

        public void Verwijderen(Database.Database database)
        {
            database.EditDatabase(String.Format("DELETE FROM PRODUCT WHERE ID = {0}", _id));
        }
    }
}