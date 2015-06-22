using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Persoon : Database.IDatabase
    {
        //Fields
        private int _id;
        private string _voornaam;
        private string _tussenvoegsel;
        private string _achternaam;
        private string _straat;
        private string _huisnr;
        private string _woonplaats;
        private string _banknr;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Tussenvoegsel
        {
            get { return _tussenvoegsel; }
            set { _tussenvoegsel = value; }
        }

        public string Voornaam
        {
            get { return _voornaam; }
            set { _voornaam = value; }
        }

        public string Achternaam
        {
            get { return _achternaam; }
            set { _achternaam = value; }
        }

        public string Straat
        {
            get { return _straat; }
            set { _straat = value; }
        }

        public string Huisnr
        {
            get { return _huisnr; }
            set { _huisnr = value; }
        }

        public string Woonplaats
        {
            get { return _woonplaats; }
            set { _woonplaats = value; }
        }

        public string Banknr
        {
            get { return _banknr; }
            set { _banknr = value; }
        }
        #endregion

        //Constructor
        public Persoon(int id, string voornaam, string tussenvoegsel, string achternaam, string straat, string woonplaats, string huisnr, string banknr)
        {
            this._id = id;
            this._voornaam = voornaam;
            this._tussenvoegsel = tussenvoegsel;
            this._achternaam = achternaam;
            this._straat = straat;
            this._woonplaats = woonplaats;
            this._huisnr = huisnr;
            this._banknr = banknr;
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