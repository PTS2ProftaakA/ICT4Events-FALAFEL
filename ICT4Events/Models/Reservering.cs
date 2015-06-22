using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Reservering : Database.IDatabase
    {
        //Fields
        private int _id;
        private Persoon _persoon;
        private DateTime _datumStart;
        private DateTime _datumEinde;
        private bool _betaald;
        private List<Plek> _plekken;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Persoon Persoon
        {
            get { return _persoon; }
            set { _persoon = value; }
        }

        public DateTime DatumStart
        {
            get { return _datumStart; }
            set { _datumStart = value; }
        }

        public DateTime DatumEinde
        {
            get { return _datumEinde; }
            set { _datumEinde = value; }
        }

        public bool Betaald
        {
            get { return _betaald; }
            set { _betaald = value; }
        }

        public List<Plek> Plekken
        {
            get { return _plekken; }
            set { _plekken = value; }
        }
        #endregion

        //Constructor
        public Reservering(int id, Persoon persoon, DateTime datumStart, DateTime datumEinde, bool betaald, List<Plek> plekken)
        {
            this._id = id;
            this._persoon = persoon;
            this._datumStart = datumStart;
            this._datumEinde = datumEinde;
            this._betaald = betaald;
            this._plekken = plekken;
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