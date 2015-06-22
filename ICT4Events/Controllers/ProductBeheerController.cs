using System;
using System.Collections.Generic;
using System.Linq;
using ICT4Events.Models;

namespace ICT4Events.Controllers
{
    public class ProductBeheerController
    {
        /// <summary>
        /// Deze klasse regelt al de omzetting van de product gui/logische laag naar de database laag.
        /// Data wordt omgezet naar objecten en andersom.
        /// </summary>

        private MasterController _masterController;

        public ProductBeheerController()
        {
            _masterController = new MasterController();
        }

        //Een procedure die alle producten ophaald uit de database.
        public List<Product> KrijgAlleProducten(List<ProductCat> categorien)
        {
            List<string>[] dataTable = _masterController.KrijgAlleProductenProcedure();
            List<Product> producten = new List<Product>();

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                producten.Add(new Product(Convert.ToInt32(dataTable[0][i]), categorien.Find(productCat => productCat.ID == Convert.ToInt32(dataTable[1][i])) as ProductCat, 
                    dataTable[2][i], dataTable[3][i], Convert.ToInt32(dataTable[4][i]), Convert.ToInt32(dataTable[5][i])));
            }

            return producten;
        }

        //Een procedure die alle categorieen ophaald uit de database.
        public List<ProductCat> KrijgAlleCategorieen()
        {
            List<string>[] dataTable = _masterController.KrijgAlleProductCatProcedure();
            List<ProductCat> productCats = new List<ProductCat>(); 

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                if (dataTable[1][i] == "")
                {
                    productCats.Add(new ProductCat(Convert.ToInt32(dataTable[0][i]), dataTable[2][i]));
                }
            }

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                if (dataTable[1][i] != "")
                {
                    productCats.Add(new ProductCat(Convert.ToInt32(dataTable[0][i]), productCats.Find(productCat => productCat.ID == Convert.ToInt32(dataTable[1][i])) as ProductCat, dataTable[2][i]));
                }
            }

            return productCats;
        }

        //Een procedure die alle productenexemplaren ophaald uit de database.
        public List<ProductExemplaar> KrijgAlleProductExemplaren(List<Product> producten)
        {
            List<string>[] dataTable = _masterController.KrijgAlleProductExemplaren();
            List<ProductExemplaar> productExemplaren = new List<ProductExemplaar>();

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                productExemplaren.Add(new ProductExemplaar(Convert.ToInt32(dataTable[0][i]), producten.Find(product => product.ID == Convert.ToInt32(dataTable[1][i])), Convert.ToInt32(dataTable[2][i]), dataTable[3][i]));
            }

            return productExemplaren;
        }

        //Een procedure die alle verhuringen ophaald uit de database.
        public List<Verhuur> KrijgAlleHuidigeVerhuringen(List<ProductExemplaar> exemplaren)
        {
            List<string>[] dataTable = _masterController.KrijgAlleHuidigeVerhuringen();

            List<Verhuur> verhuringen = new List<Verhuur>();

            for (int i = 0; i < dataTable[0].Count(); i++)
            {
                verhuringen.Add(new Verhuur(Convert.ToInt32(dataTable[0][i]), exemplaren.Find(exemplaar => exemplaar.ID == Convert.ToInt32(dataTable[1][i])), new Reservering_Polsbandje(Convert.ToInt32(dataTable[2][i]), null, null, null, false, null), Convert.ToDateTime(dataTable[3][i]), Convert.ToDateTime(dataTable[4][i]), Convert.ToInt32(dataTable[5][i]), dataTable[5][i] == "1"));
            }

            return verhuringen;
        }

        //Het verwijderen van een product.
        public void VerwijderProduct(Product product)
        {
            _masterController.VerwijderProduct(product);
        }

        //Het aanpassen van een product.
        public void AanpassenProduct(Product product)
        {
            _masterController.AanpassenProduct(product);
        }

        //Het toevoegen van een product.
        public void ToevoegenProduct(int productcatID, string merk, string serie, int typenummer, int prijs)
        {
            _masterController.ToevoegenProduct(productcatID, merk, serie, typenummer, prijs);
        }

        //Het toevoegen van een productexemplaar.
        public void ToevoegenExemplaar(Product productExemplaar)
        {
            _masterController.ToevoegenProductExemplaar(productExemplaar.ID);
        }

        //Het verwijderen van een productexemplaar.
        public void VerwijderProductExemplaar(ProductExemplaar productExemplaar)
        {
            _masterController.VerwijderProductExemplaar(productExemplaar);
        }

        //Het verwijderen van een verhuring.
        public void VerwijderVerhuring(Verhuur verhuring)
        {
            _masterController.VerwijderVerhuring(verhuring);
        }
    }
}