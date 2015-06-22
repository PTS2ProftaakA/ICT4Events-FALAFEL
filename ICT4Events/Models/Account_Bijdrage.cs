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
            //Bij de Query maak je gebruik van het huidige 'Account_Bijdrage'
            int iLike = 0;
            int iOngewenst = 0;
            if (_like)
                iLike = 1;
            if (_ongewenst)
                iOngewenst = 1;
            string query = String.Format(@"INSERT INTO ACCOUNT_BIJDRAGE(""account_id"", ""bijdrage_id"", ""like"", ""ongewenst"") VALUES({0}, {1}, {2}, {3})", _account.ID, _bijdrage.ID, iLike, iOngewenst);
            database.EditDatabase(query);
        }

        public void Aanpassen(Database.Database database)
        {
            int iLike = 0;
            int iOngewenst = 0;
            if (_like)
                iLike = 1;
            if (_ongewenst)
                iOngewenst = 1;
            string query = String.Format(@"UPDATE ACCOUNT_BIJDRAGE SET ""like"" = {0}, ""ongewenst"" = {1} WHERE ""account_id"" = {2} AND ""bijdrage_id"" = {3}",
                iLike, iOngewenst, _account.ID, _bijdrage.ID);
            database.EditDatabase(query);
        }

        public void Verwijderen(Database.Database database)
        {
            //Hier ga je een record verwijderen van Account_Bijdrage
            string query = String.Format(@"DELETE FROM ACCOUNT_BIJDRAGE WHERE ""account_id"" = {0} AND ""bijdrage_id"" = {1}", _account.ID, _bijdrage.ID);
            database.EditDatabase(query);
        }
    }
}