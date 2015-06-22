using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ICT4Events.UserControls
{
    public class BestelRegelBeheerder : Panel
    {
        //Fields
        private int _huidigeHoeveelheid;
        private int _productID;
        private int _maxHoeveelheid;
        private string _productNaam;
        private Literal hoeveelheid;
        private Button _btnPlus;
        private Button _btnMin;
        private Button _btnVerwijder;

        //Properties
        #region Properties
        public Button BtnVerwijder
        {
            get { return _btnVerwijder; }
            set { _btnVerwijder = value; }
        }
        public Button BtnPlus
        {
            get { return _btnPlus; }
            set { _btnPlus = value; }
        }

        public Button BtnMin
        {
            get { return _btnMin; }
            set { _btnMin = value; }
        }

        public Literal Hoeveelheid
        {
            get { return hoeveelheid; }
            set { hoeveelheid = value; }
        }

        public int HuidigeHoeveelheid
        {
            get { return _huidigeHoeveelheid; }
            set { _huidigeHoeveelheid = value; }
        }

        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        public string ProductNaam
        {
            get { return _productNaam; }
            set { _productNaam = value; }
        }
        public int MaxHoeveelheid
        {
            get { return _maxHoeveelheid; }
            set { _maxHoeveelheid = value; }
        }
        #endregion

        //Constructor
        public BestelRegelBeheerder(int huidigeHoeveelheid, int productId, string productNaam, int maxHoeveelheid)
        {
            // Initialiseer de fields die je mee krijgt
            _huidigeHoeveelheid = huidigeHoeveelheid;
            _productID = productId;
            _productNaam = productNaam;
            _maxHoeveelheid = maxHoeveelheid;

            //Voeg een tekst toe met de naam van het product 
            this.Controls.Add(new Literal{Text = productNaam});

            //Voeg een button toe waardoor je de hoeveelheid exemolaren op kan hogen
            _btnPlus = new Button();
            _btnPlus.ID = "btnPlus" + productId;
            _btnPlus.Text = "+";
            this.Controls.Add(_btnPlus);

            //Voeg een tekst toe de die hoeveelheid exemplaren geeft die je wil bestellen
            hoeveelheid = new Literal{Text = huidigeHoeveelheid.ToString()};
            this.Controls.Add(hoeveelheid);

            //Voeg een button toe waardoor je de hoeveelheid exemplaren kan verminderen
            _btnMin = new Button();
            _btnMin.Text = "-";
            _btnMin.ID = "btnMin" + productId;
            this.Controls.Add(_btnMin);

            //Voeg een button toe die dit control kan verwijderen uit de lijst waar hij in komt te staan
            _btnVerwijder = new Button();
            _btnVerwijder.Text = " verwijder";
            _btnVerwijder.ID = "btnVerwijder" + productId;
            this.Controls.Add(_btnVerwijder);
        }
    }
}