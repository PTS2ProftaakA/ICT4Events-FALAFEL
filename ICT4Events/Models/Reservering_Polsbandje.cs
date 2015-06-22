using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Reservering_Polsbandje : Database.IDatabase
    {
        //Fields
        private int _id;
        private Reservering _reservering;
        private Polsbandje _polsbandje;
        private Account _account;
        private bool _aanwezig;
        private List<Verhuur> _verhuren;

        //Properties
        #region Properties
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Reservering Reservering
        {
            get { return _reservering; }
            set { _reservering = value; }
        }

        public Polsbandje Polsbandje
        {
            get { return _polsbandje; }
            set { _polsbandje = value; }
        }

        public Account Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public bool Aanwezig
        {
            get { return _aanwezig; }
            set { _aanwezig = value; }
        }

        public List<Verhuur> Verhuren
        {
            get { return _verhuren; }
            set { _verhuren = value; }
        }
        #endregion

        //Constructor
        public Reservering_Polsbandje(int id, Reservering reservering, Polsbandje polsbandje, Account account, bool aanwezig, List<Verhuur> verhuren)
        {
            this._id = id;
            this._reservering = reservering;
            this._polsbandje = polsbandje;
            this._account = account;
            this._aanwezig = aanwezig;
            this._verhuren = verhuren;
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