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
    public partial class MediaSharing : System.Web.UI.Page
    {

        Database.Database database = new Database.Database();
        TijdlijnController tlc = new TijdlijnController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Account.Get(HttpContext.Current.User.Identity.Name, database) == null)
            {
                Response.Redirect("Inloggen.aspx");
            }


            //Hier voegen we een click event toe als we een file uploaden
            fileSubmit.ServerClick += btnMaakBestand_OnClick;
            database.Connect();

            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);
            if (!IsPostBack)
            {
                //Als het geen postback is maak je de placeholder van de categorie met reacties onzichtbaar
                phCategorieReacties.Visible = false;
                RefreshCat();
                RefreshExtensions();
            }
            if (Session["SelectedCategorie"] != null)
            {
                //Hier zorgen we ervoor dat de placeholder van de categorie met reacties goed worden weergeven
                phCategorieReacties.Visible = true;
                int id = (int)Session["SelectedCategorie"];
                lbCatNaam.Text = (string)Session["SelectedCategorieNaam"];
                lbCounter.Text = Convert.ToString(tlc.GetLikes(id));
                bool liked = tlc.AlreadyExists(id, acc.ID, "like");
                bool reported = tlc.AlreadyExists(id, acc.ID, "ongewenst");
                btnLike.Text = liked ? "Unlike" : "Like";
                btnRaporteren.Text = reported ? "Gerapporteerd" : "Rapporteer";
                AddBerichten(id);
            }
            RefreshBestanden("", "");
        }


        protected void btnLiken_Click(object sender, EventArgs e)
        {
            //Omdat het dynamically gecreëert is zetten we de sender als een knop
            Button btnLiken = sender as Button;

            //TODO Haal hier je Account uit een cookie/session
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);
            //Haal het bestand op, dit doen we met het ID van de button.
            string id = btnLiken.ID.Substring(1);
            int nr = Convert.ToInt32(id);
            Bestand b = Bestand.Get(nr, database);

            //Kijk of de post al geliked en/of gerapporteerd is.
            bool liked = tlc.AlreadyExists(b.ID, acc.ID, "like");
            bool reported = tlc.AlreadyExists(b.ID, acc.ID, "ongewenst");

            //Je maakt hier de like/ongewenst aan of verwijder je hem.

            if (!liked)
            {
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, b, true, reported);
                //Hier voeg je hem toe aan de database of pas je hem aan.
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
                    //Hier zet je de text van de knop andersom voor de zekerheid.
                    btnLiken.Text = "Unlike";
                }
                catch (Exception ex)
                {
                    Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                }
            }
            else if (liked)
            {
                try
                {
                    Account_Bijdrage ab = new Account_Bijdrage(1, acc, b, false, reported);
                    //Hier verwijder je hem van de database of pas je hem aan.
                    if (!reported)
                    {
                        ab.Verwijderen(database);
                    }
                    else
                    {
                        ab.Like = false;
                        ab.Aanpassen(database);
                    }
                    btnLiken.Text = "Like";
                }
                catch (Exception ex)
                {
                    Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                }
            }
            //Hier zoek je het juiste label en dan zet je de likes goed.
            Label l = (Label)phBestand.FindControl("A" + Convert.ToString(b.ID));
            l.Text = Convert.ToString(tlc.GetLikes(b.ID));

            RefreshBestanden("", "");
        }

        //Deze lijkt heel erg veel op liken.
        protected void btnRaporteren_Click(object sender, EventArgs e)
        {
            Button btnRaporteren = sender as Button;

            //Haal hier je Account uit een cookie/session
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);

            //Hier haal je het bestand op
            string id = btnRaporteren.ID.Substring(1);
            int nr = Convert.ToInt32(id);
            Bestand b = Bestand.Get(nr, database);


            bool liked = tlc.AlreadyExists(b.ID, acc.ID, "like");
            bool reported = tlc.AlreadyExists(b.ID, acc.ID, "ongewenst");
            //Hier maak je het bijdrage aan

            if (!reported)
            {
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, b, liked, true);
                //Hier voeg je hem toe
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
                //Hier verwijder of pas je hem aan.
                try
                {
                    if (!liked)
                    {
                        ab.Verwijderen(database);
                    }
                    else
                    {
                        ab.Ongewenst = false;
                        ab.Aanpassen(database);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
                }
                btnRaporteren.Text = "Rapporteer";
            }

            RefreshBestanden("", "");
        }

        public void DesignFile(Bestand b)
        {
            //Maak het panel aan
            Panel p = new Panel();
            p.BorderColor = Color.Black;
            //p.BorderStyle = BorderStyle.Solid;
            //p.BorderWidth = 1;
            p.Height = 250;
            //p.Width = 640;

            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);

            //Check of er al een Like is -- Voor nu gebruik ik user ID 4, dit moet later worden vervangen door het echte user id
            bool liked = tlc.AlreadyExists(b.ID, acc.ID, "like");
            bool reported = tlc.AlreadyExists(b.ID, acc.ID, "ongewenst");
            //Zet het juiste bestand erin.
            string extension = Path.GetExtension(b.BestandsLocatie);
            string path = Path.Combine("/TEST/", b.BestandsLocatie);
            //Voeg het bestandsnaam zonder extensie toe
            Label l = new Label();
            l.Text = Path.GetFileNameWithoutExtension(b.BestandsLocatie);
            p.Controls.Add(l);
            p.Controls.Add(new LiteralControl("<br />"));
            //Hier kijken we welke extensie het is en handelen we het zo af.
            if (extension == ".jpg" || extension == ".png")
            {
                var i = new System.Web.UI.WebControls.Image { ImageUrl = path };
                i.AlternateText = b.BestandsLocatie;
                i.Height = 100;
                p.Controls.Add(i);
                p.Height = 250;
            }
            else if (extension == ".mp3")
            {
                var s = new LiteralControl(@"<audio src=""" + path + @""" controls=""controls""></audio> ");
                p.Controls.Add(s);
                p.Height = 175;
            }
            else if (extension == ".mp4")
            {
                var v = new LiteralControl(@"<video height=""100"" controls><source src=""" + path + @"""></video>");
                p.Controls.Add(v);
                p.Height = 250;
            }
            else if (extension == ".txt")
            {
                Label lblTxt = new Label();
                string pathToBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                try
                {
                    using (StreamReader sr = new StreamReader(pathToBaseDirectory + path))
                    {
                        string line = sr.ReadToEnd();
                        lblTxt.Text = line;
                        p.Controls.Add(lblTxt);
                        p.Height = 250;
                        //Console.WriteLine(line);
                    }
                }
                catch (Exception e)
                {
                    Response.Write("<script>alert('Er is iets misgegaan met het bestand uitlezen')</script>");
                    return;
                }
            }
            else
            {
                //Als het nog niet is gevonden in alle hierboven genoemde extensies.
                p.Height = 150;
            }

            //De buttons voor een bestand
            Button btnLike = new Button();
            //De tekst moet zijn Like als deze gebruiker deze post nog niet heeft leuk gevonden, anders moet het unlinke zijn.
            btnLike.Text = liked ? "Unlike" : "Like";
            btnLike.ID = "L" + Convert.ToString(b.ID);
            btnLike.Click += btnLiken_Click;
            Button btnRapport = new Button();
            btnRapport.Text = reported ? "Gerapporteerd" : "Rapporteer";
            btnRapport.ID = "R" + Convert.ToString(b.ID);
            btnRapport.Click += btnRaporteren_Click;
            //Het aantal likes Label
            Label aantalLikesLabel = new Label();
            aantalLikesLabel.Text = Convert.ToString(tlc.GetLikes(b.ID));
            aantalLikesLabel.ID = "A" + Convert.ToString(b.ID);
            Button btnGoToPost = new Button();
            btnGoToPost.Text = "Ga naar post";
            btnGoToPost.ID = "G" + Convert.ToString(b.ID);
            btnGoToPost.Click += btnGoToPost_Click;
            Button btnDownload = new Button();
            btnDownload.Text = "Download";
            btnDownload.ID = "DWN" + Convert.ToString(b.ID);
            btnDownload.Click += btnDownload_Click;

            //Hier voeg je ze letterlijk toe aan de panel
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(btnLike);
            p.Controls.Add(aantalLikesLabel);
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(btnRapport);
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(btnGoToPost);
            p.Controls.Add(new LiteralControl("<br />"));
            p.Controls.Add(btnDownload);

            //Hier voegen we de panel toe aan de place holder
            phBestand.Controls.Add(p);
            phBestand.Controls.Add(new LiteralControl("<br />"));
        }

        void btnDownload_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //Resharper wil wat zekerheid.
            if (btn != null)
            {
                //Met het id van de button het id van het bestand krijgen
                int id = Convert.ToInt32(btn.ID.Substring(3));

                //Hier halen we de bestandslocatie van het bestand op
                List<string>[] table;
                string query = String.Format(@"SELECT ""bestandslocatie"" FROM BESTAND WHERE ""bijdrage_id"" = {0}", id);
                List<string> kolommen = new List<string>();
                kolommen.Add("BESTANDSLOCATIE");

                table = database.SelectQuery(query, kolommen);

                if (table[0].Count > 1)
                {
                    string locatie = Convert.ToString(table[0][1]);
                    if (!String.IsNullOrEmpty(locatie) && File.Exists(Path.Combine("/TEST/", locatie)))
                    {
                        //Hier downloaden we het bestand.
                        Response.AddHeader("content-disposition", "attachment; filename=" + Path.GetFileName(locatie));
                        Response.WriteFile(Path.Combine("/TEST/", locatie));
                        Response.End();
                    }
                }
            }
        }

        void btnGoToPost_Click(object sender, EventArgs e)
        {
            //Hier sla je het bestand in een sessie op en ga je daarna naar de MediaPost pagina
            Button btn = sender as Button;
            int ID = Convert.ToInt32(btn.ID.Substring(1));
            Bestand b = Bestand.Get(ID, database);
            Session["Post"] = b;
            Response.Redirect("MediaPost.aspx");
        }

        protected void btnMaakCategorie_OnClick(object sender, EventArgs e)
        {
            try
            {
                List<Categorie> categorieList = tlc.HaalAlleOpCategorieën();
                string catText = txtCategorie.Text;
                //Als het selectedValue main is dan is het een main categorie
                if (ddlCategorie.SelectedValue == "Main")
                {
                    //Kijken of de categorie al bestaat.
                    if (categorieList.Any(cat => cat.Naam == catText && cat.SubCategorie == null))
                    {
                        Response.Write("<script>alert('Deze categorie bestaat al')</script>");
                        return;
                    }
                    tlc.MaakCategorie(catText);
                }
                else
                {
                    int i = Convert.ToInt32(ddlCategorie.SelectedValue.Substring(4));
                    Categorie c = Categorie.Get(i, database);
                    //Kijken of de categorie al bestaat.
                    foreach (Categorie cat in categorieList)
                    {
                        if (cat.SubCategorie != null)
                        {
                            if (cat.Naam == catText && cat.SubCategorie.ID == c.ID)
                            {
                                Response.Write("<script>alert('Deze categorie bestaat al')</script>");
                                return;
                            }
                        }

                    }
                    tlc.MaakCategorie(catText, c);
                }
                RefreshCat();
            }
            catch (Exception ex)
            {
                Response.Write("<script type = 'text/javascript'>alert('" + ex.Message + "');</script>");
            }
        }

        public void DesignTreeView(List<Categorie> categorieList)
        {
            //Maak het eerst leeg
            ddlCategorie.Items.Clear();
            //Maak hier het Main item aan.
            ListItem liMain = new ListItem();
            liMain.Text = "Main";
            liMain.Value = "Main";
            ddlCategorie.Items.Add(liMain);
            tvCategorie.Nodes.Clear();

            //Elke categorie toevoegen aan de treeview en de categorie box
            foreach (Categorie c in categorieList)
            {
                if (c.SubCategorie == null)
                {
                    TreeNode parentTreeNode = new TreeNode();
                    parentTreeNode.Text = c.Naam;
                    parentTreeNode.Value = "PTN" + c.ID;
                    parentTreeNode.SelectAction = TreeNodeSelectAction.Select;
                    //Functie om de childrows te krijgen
                    GetChildRows(c, parentTreeNode);
                    tvCategorie.Nodes.Add(parentTreeNode);
                    ListItem li = new ListItem();
                    li.Text = c.Naam;
                    li.Value = "DDLC" + c.ID;
                    ddlCategorie.Items.Add(li);

                }
            }
        }

        private void GetChildRows(Categorie cat, TreeNode tn)
        {
            //Haal alle child records op van een hoofd categorie.
            if (tlc.GetChildRows(cat.ID) != null)
            {
                List<Categorie> childRows = tlc.GetChildRows(cat.ID);
                foreach (Categorie childCategorie in childRows)
                {
                    TreeNode childTreeNode = new TreeNode();
                    childTreeNode.Text = childCategorie.Naam;
                    childTreeNode.Value = "CTN" + childCategorie.ID;
                    childTreeNode.SelectAction = TreeNodeSelectAction.Select;
                    tn.ChildNodes.Add(childTreeNode);
                    ListItem li = new ListItem();
                    li.Text = childCategorie.Naam;
                    ddlCategorie.Items.Add(li);
                    li.Value = "DDLC" + childCategorie.ID;
                    if (tlc.GetChildRows(childCategorie.ID) != null)
                    {
                        //Blijf dit doen totdat er geen childcategorie meer is
                        GetChildRows(childCategorie, childTreeNode);
                    }
                }
            }
        }

        public void RefreshCat()
        {
            List<Categorie> categorieLijst = tlc.HaalAlleOpCategorieën();
            //Maak de treeview aan
            DesignTreeView(categorieLijst);

            //Searchbox legen
            ddlSearch.Items.Clear();
            //het "alle" item toevoegen
            ListItem li = new ListItem();
            li.Text = "Alle";
            li.Value = "AlleCategorie";
            //Alle toevoegen aan de 
            ddlSearch.Items.Add(li);
            foreach (Categorie c in categorieLijst)
            {
                li = new ListItem();
                li.Text = c.Naam;
                li.Value = "DDLS" + c.ID;
                ddlSearch.Items.Add(li);
            }
            //uitklappen.
            tvCategorie.ExpandAll();
        }

        public void RefreshBestanden(string input, string soort)
        {
            //Voor elk bestand uit de database maak iets aan in de placeholder
            List<Bestand> bestanden = null;
            //kijken wat de input was, als het niks is dan haal je alles op.
            if (input == "" && soort == "")
            {
                if (Session["Bestanden"] == null)
                {
                    bestanden = tlc.HaalOpBestanden("");
                }
                else
                {
                    bestanden = (List<Bestand>)Session["Bestanden"];
                }
            }
            else
            {
                if (soort == "Cat")
                {
                    //Als er input is dan haal je de bestanden van die categorieën op.
                    bestanden = tlc.HaalOpBestanden(input);
                    Session["Bestanden"] = bestanden;
                }
                else if (soort == "Ext")
                {
                    bestanden = tlc.HaalOpBestandenMetExtensie(input);
                    Session["Bestanden"] = bestanden;
                }

            }
            phBestand.Controls.Clear();
            if (bestanden != null)
                foreach (Bestand b in bestanden)
                {
                    //Ontwerp hier de file
                    DesignFile(b);
                }
        }

        protected void btnMaakBestand_OnClick(object sender, EventArgs e)
        {
            //Kijk of de geselecteerde categorie niet Main is.
            if (ddlCategorie.SelectedValue == "Main")
            {
                //TODO ERROR : mag niet in main posten
                Response.Write("<script>alert('Mag niet in de main categorie posten.')</script>");
                return;
            }
            //Hier maak je een nieuw bestand aan in de database
            string i = ddlCategorie.SelectedValue.Substring(4);
            Categorie c = Categorie.Get(Convert.ToInt32(i), database);

            if (fileInput.PostedFile != null && fileInput.PostedFile.ContentLength > 0)
            {
                //Haal de extensie van het bestand op.
                string extension = Path.GetExtension(fileInput.PostedFile.FileName);
                //Controleer hier op alle extensies die je er niet in wilt hebben.
                if (extension == ".gliffy")
                {
                    //TODO ERROR: Verboden extensie
                    Response.Write("<script>alert('Deze extensie is niet toegestaan.')</script>");
                    return;
                }
                string encoded = System.Security.SecurityElement.Escape(Path.GetFileName(fileInput.PostedFile.FileName));
                string targetFile = Path.Combine("\\TEST\\", Path.GetFileName(fileInput.PostedFile.FileName));
                try
                {
                    if (!File.Exists(targetFile))
                    {
                        //Maak hier het bestand echt aan en zet het in de database
                        string pathToBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        fileInput.PostedFile.SaveAs(pathToBaseDirectory + targetFile);
                        tlc.MaakBestand(c, encoded);
                    }
                    else
                    {
                        Response.Write("<script>alert('Dit bestand bestaat al.')</script>");
                        return;
                    }
                }
                catch (System.Web.HttpException ex)
                {
                    Response.Write("<script>alert('Het uploaden is mislukt.')</script>");
                    return;
                }
            }
            RefreshBestanden("", "");
        }

        protected void btnVerwijderCategorie_OnClick(object sender, EventArgs e)
        {
            //Als het main is, doe het niet.
            if (ddlCategorie.SelectedValue == "Main")
            {
                Response.Write("<script>alert('U mag de Main categorie niet verwijderen.')</script>");
                return;
            }
            //Anders verwijder je de categorie, hier wordt niet op de persoon gezocht.
            string i = ddlCategorie.SelectedValue.Substring(4);
            Categorie c = Categorie.Get(Convert.ToInt32(i), database);
            c.Verwijderen(database);
            //Haal de sessie weg en ga herlaad de pagina.
            Session.Remove("SelectedCategorie");
            Response.Redirect("MediaSharingOmgeving.aspx");
        }

        protected void tvCategorie_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            //hier zet je de placeholder visible en zet je de data erin van de categorie die je geselecteerd hebt
            phCategorieReacties.Visible = true;
            int id = Convert.ToInt32(tvCategorie.SelectedValue.Substring(3));
            lbCatNaam.Text = tvCategorie.SelectedNode.Text;
            lbCounter.Text = Convert.ToString(tlc.GetLikes(id));
            Session["SelectedCategorie"] = id;
            Session["SelectedCategorieNaam"] = lbCatNaam.Text;
            AddBerichten(id);


        }

        protected void btnLikeCategorie_OnClick(object sender, EventArgs e)
        {
            //Haal hier je Account uit een cookie/session
            //TODO ACCOUNT
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);

            //Haal het id uit de sessie
            int id = (int)Session["SelectedCategorie"];
            Categorie c = Categorie.Get(id, database);


            bool liked = tlc.AlreadyExists(c.ID, acc.ID, "like");
            bool reported = tlc.AlreadyExists(c.ID, acc.ID, "ongewenst");

            //Hier zet je de text van de knop andersom.
            if (!liked)
            {
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, c, true, reported);
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
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, c, false, reported);
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
            lbCounter.Text = Convert.ToString(tlc.GetLikes(id));
            AddBerichten(id);
        }

        protected void btnRaporterenCategorie_OnClick(object sender, EventArgs e)
        {
            //TODO haal het account uit de sessie.
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);

            //Haal het id op van de categorie
            int id = Convert.ToInt32(tvCategorie.SelectedValue.Substring(3));
            Categorie c = Categorie.Get(id, database);

            bool liked = tlc.AlreadyExists(c.ID, acc.ID, "like");
            bool reported = tlc.AlreadyExists(c.ID, acc.ID, "ongewenst");

            if (!reported)
            {
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, c, liked, true);
                //Voeg het account bijdrage toe of pas hem aan.
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
                Account_Bijdrage ab = new Account_Bijdrage(1, acc, c, liked, false);
                //Verwijder of pas het account bijdrage aan
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
            AddBerichten(id);
        }

        protected void btnReageerCategorie_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tvCategorie.SelectedValue.Substring(3));

            //Check de lengte van de tekst
            string inhoud = txtInhoud.Text;
            if (inhoud.Length <= 255 && txtTitel.Text.Length <= 255)
            {
                //Kijk of er een titel is, zo ja voeg toe met titel, zo niet dan voeg toe zonder.
                if (txtTitel.Text != null)
                {
                    string titel = txtTitel.Text;
                    tlc.MaakBericht(inhoud, titel, id);
                }
                else
                {
                    tlc.MaakBericht(inhoud, null, id);
                }
                txtTitel.Text = "";
                txtInhoud.Text = "";

                AddBerichten(id);
            }
            else
            {
                //Hier komt hij als de lengte te lang is.
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


        public void AddBerichten(int id)
        {
            //Maak eerst de placeholder leeg
            phBerichten.Controls.Clear();
            //Haal alle berichten op
            List<Bericht> berichten = tlc.Getberichten(id);
            //Als het niet leeg is dan:
            if (berichten != null)
            {
                Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);
                //Maak de juiste controls voor elk bericht en voeg deze toe
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
                    btnLike.ID = "LCatLike" + Convert.ToString(br.ID);
                    btnLike.Click += btnLikeCategorieBericht_Click;
                    btnLike.CausesValidation = false;

                    Button btnRapport = new Button();
                    btnRapport.Text = reported ? "Gerapporteerd" : "Rapporteer";
                    btnRapport.ID = "RCatRept" + Convert.ToString(br.ID);
                    btnRapport.Click += btnRapportCategorieBericht_Click;
                    btnRapport.CausesValidation = false;

                    //Het aantal likes Label
                    Label aantalLikesLabel = new Label();
                    aantalLikesLabel.Text = Convert.ToString(tlc.GetLikes(br.ID));
                    aantalLikesLabel.ID = "A" + Convert.ToString(br.ID);

                    Button btnReactie = new Button();
                    btnReactie.Text = "Ga naar reactie";
                    btnReactie.ID = "REACTIE" + br.ID;
                    btnReactie.Click += btnReactieCategorieBericht_Click;
                    btnReactie.CausesValidation = false;

                    //Button voor het verwijderen
                    if (br.Account.ID == acc.ID)
                    {
                        #region Controls met verwijderen toevoegen
                        Button btnBerichtVerwijderen = new Button();
                        btnBerichtVerwijderen.Text = "X";
                        btnBerichtVerwijderen.Font.Size = new FontUnit("30px");
                        btnBerichtVerwijderen.ID = "VCatBer" + br.ID;
                        btnBerichtVerwijderen.Click += btnBerichtVerwijderenCategorieBericht_Click;
                        btnBerichtVerwijderen.CausesValidation = false;

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
                        #endregion
                    }
                    else
                    {
                        #region Controls toevoegen
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
                        #endregion
                    }
                }
            }
        }

        private void btnBerichtVerwijderenCategorieBericht_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //Haal het id van de button
            int id = Convert.ToInt32(btn.ID.Substring(7));
            //Haal het bericht met het id
            Bericht b = Bericht.Get(id, database);
            //Verwijder het, dit kan door de on delete cascade makkelijk.
            b.Verwijderen(database);

            int cId = Convert.ToInt32(tvCategorie.SelectedValue.Substring(3));
            AddBerichten(cId);
        }

        private void btnReactieCategorieBericht_Click(object sender, EventArgs e)
        {
            //Ga naar de reactie als je erop klikt bij een categorie reactie
            Button btn = sender as Button;
            int bid = Convert.ToInt32(btn.ID.Substring(7));
            Bericht b = Bericht.Get(bid, database);
            Session["Reactie"] = b;
            Response.Redirect("~/ReactiePost.aspx");
        }

        private void btnRapportCategorieBericht_Click(object sender, EventArgs e)
        {
            Button btnSender = sender as Button;

            //Haal het account uit de sessie
            //TODO ACCOUNT
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);

            if (btnSender != null)
            {
                string id = btnSender.ID.Substring(8);
                int nr = Convert.ToInt32(id);
                Bericht b = Bericht.Get(nr, database);

                bool liked = tlc.AlreadyExists(b.ID, acc.ID, "like");
                bool reported = tlc.AlreadyExists(b.ID, acc.ID, "ongewenst");

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
            }
            int cId = Convert.ToInt32(tvCategorie.SelectedValue.Substring(3));
            AddBerichten(cId);
        }

        private void btnLikeCategorieBericht_Click(object sender, EventArgs e)
        {
            Button btnSender = sender as Button;

            //Haal hier je Account uit een cookie/session
            //TODO ACCOUNT
            Account acc = Account.Get(HttpContext.Current.User.Identity.Name, database);
            //Haal het bestand op.
            if (btnSender != null)
            {
                string id = btnSender.ID.Substring(8);
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
            int cId = (int)Session["SelectedCategorie"];
            AddBerichten(cId);
        }

        protected void ddlSearch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string naam = ddlSearch.SelectedItem.Text;
            if (naam != "Alle")
            {
                RefreshBestanden(naam, "Cat");
            }
            else
            {
                if (Session["Bestanden"] != null)
                {
                    Session.Remove("Bestanden");
                }
                RefreshBestanden("", "");
            }
        }

        protected void ddlExtensionSearch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string naam = ddlExtensionSearch.SelectedItem.Text;
            if (naam != "Alle")
            {
                RefreshBestanden(naam, "Ext");
            }
            else
            {
                if (Session["Bestanden"] != null)
                {
                    Session.Remove("Bestanden");
                }
                RefreshBestanden("", "");
            }
        }

        public void RefreshExtensions()
        {
            List<string> extensies = tlc.GetExtensions();
            ddlExtensionSearch.Items.Clear();
            ListItem li = new ListItem();
            li.Text = "Alle";
            li.Value = "AlleExtensies";
            //Alle toevoegen aan de 
            ddlExtensionSearch.Items.Add(li);
            foreach (string s in extensies)
            {
                li = new ListItem();
                li.Text = s;
                li.Value = s;
                ddlExtensionSearch.Items.Add(li);
            }
        }
    }
}