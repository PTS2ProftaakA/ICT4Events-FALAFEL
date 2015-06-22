using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Events.Models
{
    public class Account_Bijdrage : Database.IDatabase
    {
        //Fields
        private int _id;
        private Account _account;
        private Bijdrage _bijdrage;
        private bool _like;
        private bool _ongewenst;

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

        public Bijdrage Bijdrage
        {
            get { return _bijdrage; }
            set { _bijdrage = value; }
        }

        public bool Like
        {
            get { return _like; }
            set { _like = value; }
        }

        public bool Ongewenst
        {
            get { return _ongewenst; }
            set { _ongewenst = value; }
        }
        #endregion

        //Constructor
        public Account_Bijdrage(int id, Account account, Bijdrage bijdrage, bool like, bool ongewenst)
        {
            this._id = id;
            this._account = account;
            this._bijdrage = bijdrage;
            this._like = like;
            this._ongewenst = ongewenst;
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