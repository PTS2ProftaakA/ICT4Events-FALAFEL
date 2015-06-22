using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public abstract class Bijdrage : Database.IDatabase
    {
        public enum  BijdrageType
        {
            Categorie,
            Bestand,
            Bericht
        }

        //Fields
        private int _id;
        private Account _account;
        private DateTime _datum;
        private BijdrageType _soort;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Account Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public DateTime Datum
        {
            get { return _datum; }
            set { _datum = value; }
        }

        public BijdrageType Soort
        {
            get { return _soort; }
            set { _soort = value; }
        }
        #endregion

        //Constructor
        public Bijdrage(int id, Account account, DateTime datum, BijdrageType soort)
        {
            this._id = id;
            this._account = account;
            this._datum = datum;
            this._soort = soort;
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