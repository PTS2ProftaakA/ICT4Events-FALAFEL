using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Events.Controller;
using ICT4Events.Models;

namespace ICT4Events
{
    public partial class MediaPost : System.Web.UI.Page
    {
        Database.Database database = new Database.Database();
        TijdlijnController tlc = new TijdlijnController();
        private Bestand _b;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Account.Get(HttpContext.Current.User.Identity.Name, database) == null)
            {
                Response.Redirect("Inloggen.aspx");
            }
            
            //Haal hier het bestand uit de session
            if (Session["Post"] != null)
            {
                _b = (Bestand) Session["Post"];
                DesignFile();
            }
            else
            {
                Response.Redirect("MediaSharing.aspx");
            }
        }

        public void DesignFile()
        {
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);
            //Check of er al een Like is -- Voor nu gebruik ik user ID 4, dit moet later worden vervangen door het echte user id
            bool liked = tlc.AlreadyExists(_b.ID, acc.ID, "like");
            bool reported = tlc.AlreadyExists(_b.ID, acc.ID, "ongewenst");
            //Username weergeven
            lbUsername.Text = _b.Account.Gebruikersnaam;
            pnlBijdrage.Controls.Clear();

            //Zet het juiste bestand erin.
            string extension = Path.GetExtension(_b.BestandsLocatie);
            string pathToBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine("TEST\\", _b.BestandsLocatie);
            //Voeg het bestandsnaam zonder extensie toe
            Label l = new Label();
            l.Text = Path.GetFileNameWithoutExtension(_b.BestandsLocatie);
            pnlBijdrage.Controls.Add(l);
            pnlBijdrage.Controls.Add(new LiteralControl("<br />"));
            //Hier kijken we welke extensie het is en handelen we het zo af.
            if (extension == ".jpg" || extension == ".png")
            {
                var i = new System.Web.UI.WebControls.Image { ImageUrl = path };
                i.AlternateText = _b.BestandsLocatie;
                i.Height = 200;
                pnlBijdrage.Controls.Add(i);
                pnlBijdrage.Height = 250;
            }
            else if (extension == ".mp3")
            {
                var s = new LiteralControl(@"<audio src=""" + path + @""" controls=""controls""></audio> ");
                pnlBijdrage.Controls.Add(s);
                pnlBijdrage.Height = 175;
            }
            else if (extension == ".mp4")
            {
                var v = new LiteralControl(@"<video height=""200"" controls><source src=""" + path + @"""></video>");
                pnlBijdrage.Controls.Add(v);
                pnlBijdrage.Height = 250;
            }
            else if (extension == ".txt")
            {
                Label lblTxt = new Label();
                try
                {
                    using (StreamReader sr = new StreamReader(pathToBaseDirectory + path))
                    {
                        string line = sr.ReadToEnd();
                        lblTxt.Text = line;
                        pnlBijdrage.Controls.Add(lblTxt);
                        pnlBijdrage.Height = 250;
                    }
                }
                catch (Exception e)
                {
                    Response.Write("<script>alert('Er is iets misgegaan met de tekst uitlezen.')</script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script>alert('Deze Extensie wordt niet herkent')</script>");
                Response.Redirect("MediaSharing.aspx");
            }

            if (_b.Account.ID == acc.ID)
            {
                phVerwijder.Controls.Clear();
                Button btnVerwijderen = new Button();
                btnVerwijderen.Text = "X";
                btnVerwijderen.Font.Size = new FontUnit("30px");
                btnVerwijderen.ID = "btnVerwijder";
                btnVerwijderen.Click += btnVerwijderen_Click;
                phVerwijder.Controls.Add(btnVerwijderen);
            }

            //De tekst moet zijn Like als deze gebruiker deze post nog niet heeft leuk gevonden, anders moet het unlinke zijn.
            btnLike.Text = liked ? "Unlike" : "Like";
            btnRaporteren.Text = reported ? "Gerapporteerd" : "Rapporteer";

            //Het aantal likes Label
            lbCounter.Text = Convert.ToString(tlc.GetLikes(_b.ID));

            AddBerichten();
        }

        void btnVerwijderen_Click(object sender, EventArgs e)
        {
            _b.Verwijderen(database);
            Response.Redirect("MediaSharing.aspx");
        }


        protected void btnLike_OnClick(object sender, EventArgs e)
        {
            _b = (Bestand)Session["Post"];
            //Haal hier je Account uit een cookie/session
            //TODO ACCOUNT
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);

            //Haal het bestand op.
            int nr = _b.ID;
            _b = Bestand.Get(nr, database);


            bool liked = tlc.AlreadyExists(_b.ID, acc.ID, "like");
            bool reported = tlc.AlreadyExists(_b.ID, acc.ID, "ongewenst");
            //Dit moet je werkelijk waar aanmaken, nadat je de account_bijdrage hebt aangemaakt voeg je die toe aan de database.
            //Hier zet je de text van de knop andersom.
            if (!liked)
            {
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, _b, true, reported);
                //Hier voeg je hem toe aan de database
                try
                {
                    if (!reported)
                    {
                        ab.Toevoegen(database);
                    }
                    else
                    {
                        ab.Aanpassen(database);
                    }
                    btnLike.Text = "Unlike";
                }
                catch (Exception ex)
                {
                    Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                }
            }
            else if (liked)
            {
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, _b, false, reported);
                //Hier verwijder je de Account_Bijdrage weer
                try
                {
                    if (!reported)
                    {
                        ab.Verwijderen(database);
                    }
                    else
                    {
                        ab.Aanpassen(database);
                    }
                    btnLike.Text = "Like";
                }
                catch (Exception ex)
                {
                    Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                }
            }

            DesignFile();
        }

        protected void btnRaporteren_OnClick(object sender, EventArgs e)
        {
            _b = (Bestand)Session["Post"];
            //Haal hier je Account uit een cookie/session
            //TODO ACCOUNT
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);

            bool liked = tlc.AlreadyExists(_b.ID, acc.ID, "like");
            bool reported = tlc.AlreadyExists(_b.ID, acc.ID, "ongewenst");

            database.Connect();
            if (!reported)
            {
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, _b, liked, true);
                try
                {
                    if (!liked)
                    {
                        ab.Toevoegen(database);
                    }
                    else
                    {
                        ab.Aanpassen(database);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                }
                btnRaporteren.Text = "Gerappoteerd";
            }
            else if (reported)
            {
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, _b, liked, false);
                try
                {
                    if (!liked)
                    {
                        ab.Verwijderen(database);
                    }
                    else
                    {
                        ab.Aanpassen(database);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                }
                btnRaporteren.Text = "Rapporteer";
            }

            DesignFile();
        }

        protected void btnReageer_OnClick(object sender, EventArgs e)
        {
            _b = (Bestand)Session["Post"];
            string inhoud = txtInhoud.Text;
            if (inhoud.Length <= 255 && txtTitel.Text.Length <= 255)
            {
                if (txtTitel.Text != null)
                {
                    string titel = txtTitel.Text;
                    tlc.MaakBericht(inhoud, titel, _b.ID);
                }
                else
                {
                    tlc.MaakBericht(inhoud, null, _b.ID);
                }
                txtTitel.Text = "";
                txtInhoud.Text = "";
                DesignFile();
            }
            else
            {
                string msg = "Input is te lang";
                if (inhoud.Length > 255 && txtTitel.Text.Length > 255)
                {
                    Response.Write("<script language='javascript'>alert('" + msg + "');</script>");
                }
                else if (inhoud.Length > 255)
                {
                    Response.Write("<script language='javascript'>alert('" + msg + "')</script>");
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('" + msg + "')</script>");
                }
            }
           
        }

        public void AddBerichten()
        {
            phBerichten.Controls.Clear();
            List<Bericht> berichten = tlc.Getberichten(_b.ID);
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);
            if (berichten != null)
            {
                foreach (Bericht br in berichten)
                {
                    bool liked = tlc.AlreadyExists(br.ID, acc.ID, "like");
                    bool reported = tlc.AlreadyExists(br.ID, acc.ID, "ongewenst");
                    
                    Label accountNaam = new Label();
                    accountNaam.Text = "Door: " + br.Account.Gebruikersnaam;
                    Label berichtTitel = new Label();
                    berichtTitel.Text = br.Titel;
                    berichtTitel.Font.Size = new FontUnit("20px");
                    Label berichtInhoud = new Label();
                    berichtInhoud.Text = br.Inhoud;

                    Button btnLike = new Button();
                    //De tekst moet zijn Like als deze gebruiker deze post nog niet heeft leuk gevonden, anders moet het unlinke zijn.
                    btnLike.Text = liked ? "Unlike" : "Like";
                    btnLike.ID = "L" + Convert.ToString(br.ID);
                    btnLike.Click += btnLike_Click;

                    Button btnRapport = new Button();
                    btnRapport.Text = reported ? "Gerapporteerd" : "Rapporteer";
                    btnRapport.ID = "R" + Convert.ToString(br.ID);
                    btnRapport.Click += btnRapport_Click;

                    //Het aantal likes Label
                    Label aantalLikesLabel = new Label();
                    aantalLikesLabel.Text = Convert.ToString(tlc.GetLikes(br.ID));
                    aantalLikesLabel.ID = "A" + Convert.ToString(br.ID);

                    Button btnReactie = new Button();
                    btnReactie.Text = "Ga naar reactie";
                    btnReactie.ID = "REACTIE" + br.ID;
                    btnReactie.Click += btnReactie_Click;

                    //Button voor het verwijderen
                    if (br.Account.ID == acc.ID)
                    {
                        Button btnBerichtVerwijderen = new Button();
                        btnBerichtVerwijderen.Text = "X";
                        btnBerichtVerwijderen.Font.Size = new FontUnit("30px");
                        btnBerichtVerwijderen.ID = "V" + br.ID;
                        btnBerichtVerwijderen.Click += BtnBerichtVerwijderenOnClick;

                        phBerichten.Controls.Add(accountNaam);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(berichtTitel);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(berichtInhoud);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(btnBerichtVerwijderen);
                        phBerichten.Controls.Add(btnLike);
                        phBerichten.Controls.Add(aantalLikesLabel);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(btnRapport);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(btnReactie);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                    }
                    else
                    {
                        phBerichten.Controls.Add(accountNaam);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(berichtTitel);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(berichtInhoud);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(btnLike);
                        phBerichten.Controls.Add(aantalLikesLabel);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(btnRapport);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(btnReactie);
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                        phBerichten.Controls.Add(new LiteralControl("<br />"));
                    }

                    
                }
            }
        }

        void btnReactie_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int ID = Convert.ToInt32(btn.ID.Substring(7));
            Bericht b = Bericht.Get(ID, database);
            Session["Reactie"] = b;
            Response.Redirect("~/ReactiePost.aspx");
        }

        private void BtnBerichtVerwijderenOnClick(object sender, EventArgs eventArgs)
        {
            Button btn = sender as Button;
            string id = btn.ID.Substring(1);
            Bericht b = Bericht.Get(Convert.ToInt32(id), database);
            b.Verwijderen(database);
            Response.Redirect("MediaPost.aspx");
        }

        void btnRapport_Click(object sender, EventArgs e)
        {
            Button btnSender = sender as Button;
            //TODO ACCOUNT
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);

            if (btnSender != null)
            {
                string id = btnSender.ID.Substring(1);
                int nr = Convert.ToInt32(id);
                Bericht b = Bericht.Get(nr, database);

                bool liked = tlc.AlreadyExists(b.ID, acc.ID, "like");
                bool reported = tlc.AlreadyExists(b.ID, acc.ID, "ongewenst");
                //Dit moet je werkelijk waar aanmaken, nadat je de account_bijdrage hebt aangemaakt voeg je die toe aan de database.
                if (!reported)
                {
                    Account_Bijdrage ab = new Account_Bijdrage(1, acc, b, liked, true);
                    try
                    {
                        if (!liked)
                        {
                            ab.Toevoegen(database);
                        }
                        else
                        {
                            ab.Aanpassen(database);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                    }
                    btnRaporteren.Text = "Gerappoteerd";
                }
                else if (reported)
                {
                    Account_Bijdrage ab = new Account_Bijdrage(1, acc, b, liked, false);
                    try
                    {
                        if (!liked)
                        {
                            ab.Verwijderen(database);
                        }
                        else
                        {
                            ab.Aanpassen(database);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                    }
                    btnRaporteren.Text = "Rapporteer";
                }
                Response.Redirect("MediaPost.aspx");
            }
        }

        void btnLike_Click(object sender, EventArgs e)
        {
            Button btnSender = sender as Button;

            //Haal hier je Account uit een cookie/session
            //TODO ACCOUNT
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);
            //Haal het bestand op.
            if (btnSender != null)
            {
                string id = btnSender.ID.Substring(1);
                int nr = Convert.ToInt32(id);
                Bericht b = Bericht.Get(nr, database);


                bool liked = tlc.AlreadyExists(b.ID, acc.ID, "like");
                bool reported = tlc.AlreadyExists(b.ID, acc.ID, "ongewenst");
                
                //Hier zet je de text van de knop andersom.
                if (!liked)
                {
                    Account_Bijdrage ab = new Account_Bijdrage(1, acc, b, true, reported);
                    //Hier voeg je hem toe aan de database
                    try
                    {
                        if (!reported)
                        {
                            ab.Toevoegen(database);
                        }
                        else
                        {
                            ab.Aanpassen(database);
                        }
                        btnLike.Text = "Unlike";
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                    }
                }
                else if (liked)
                {
                    Account_Bijdrage ab = new Account_Bijdrage(1, acc, b, false, reported);
                    //Hier verwijder je de Account_Bijdrage weer
                    try
                    {
                        if (!reported)
                        {
                            ab.Verwijderen(database);
                        }
                        else
                        {
                            ab.Aanpassen(database);
                        }
                        btnLike.Text = "Like";
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                    }
                }
            }

            Response.Redirect("MediaPost.aspx");
        }
    }
}