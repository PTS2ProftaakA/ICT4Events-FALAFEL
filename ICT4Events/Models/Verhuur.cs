using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Verhuur : Database.IDatabase
    {
        //Fields
        private int _id;
        private ProductExemplaar _productExemplaar;
        private Reservering_Polsbandje _reserveringPolsbandje;
        private DateTime _datumIn;
        private DateTime _datumUit;
        private int _prijs;
        private bool _betaald;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public ProductExemplaar ProductExemplaar
        {
            get { return _productExemplaar; }
            set { _productExemplaar = value; }
        }

        public Reservering_Polsbandje ReserveringPolsbandje
        {
            get { return _reserveringPolsbandje; }
            set { _reserveringPolsbandje = value; }
        }

        public DateTime DatumIn
        {
            get { return _datumIn; }
            set { _datumIn = value; }
        }

        public DateTime DatumUit
        {
            get { return _datumUit; }
            set { _datumUit = value; }
        }

        public int Prijs
        {
            get { return _prijs; }
            set { _prijs = value; }
        }

        public bool Betaald
        {
            get { return _betaald; }
            set { _betaald = value; }
        }
        #endregion

        //Constructors
        public Verhuur(int id, ProductExemplaar productExemplaar, Reservering_Polsbandje reserveringPolsbandje, DateTime datumIn, DateTime datumUit, int prijs, bool betaald)
        {
            this._id = id;
            this._productExemplaar = productExemplaar;
            this._reserveringPolsbandje = reserveringPolsbandje;
            this._datumIn = datumIn;
            this._datumUit = datumUit;
            this._prijs = prijs;
            this._betaald = betaald;
        }

        public void Toevoegen(Database.Database database)
        {

        }

        public void Aanpassen(Database.Database database)
        {

        }

        public void Verwijderen(Database.Database database)
        {
            database.EditDatabase(String.Format("DELETE FROM VERHUUR WHERE ID = {0}", _id));
        }
    }
}