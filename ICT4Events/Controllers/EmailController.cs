using System;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mime;
using BarcodeLib;

namespace ICT4Events.Controllers
{
    public class EmailController
    {
        public void eMail(string authenticatieCode, string emailadres, string gebruikersnaam, string barcodeString)
        {
            //Bestandslocatie waar het plaatje van de barcode word opgeslagen.
            string bestandslocatie = "C:/Users/Martijn/Desktop/testBarcode.jpg";

            //Het aanmaken van een plaatje van een barcode.
            //Deze barcode staat gelijk aan de barcode van het polsbandje van het account.
            Image barcodeImage = BarcodeGenereren(barcodeString);

            //Het locaal opslaan van het plaatje.
            //De locatie kan veranderd worden door de bestandslocatie aan te passen.
            barcodeImage.Save(bestandslocatie, ImageFormat.Jpeg);

            //SMTP client die de mail verstuurd.
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            
            //Een bericht aanmaken.
            MailMessage mail = new MailMessage();

            //De inhoud van het bericht.
            //De html is aangezet zodat er opmaak toegepast kan worden.
            string htmlBody = String.Format("Geachte {0},<br /> <br /> Je hebt deze mail gekregen om je te verifiëren voor het evenement." +
                                      "<br /> <br /> <a href=\"http://localhost:1687/GUI_Verificatie.aspx/{1}\">Klik hier om te verifiëren.</a>" +
                                      "<br /> <br /> Hieronder staat de barcode die je polsbandje zal hebben op het evenement," +
                                      "<br /> Laat hem scannen bij de incheckbalie om je polsbandje te verkrijgen." +
                                      "<br /> <br /><img src=\"cid:pictureBarcode\">" +
                                      "<br /> <br /> We hopen je te zien bij het evenement, " +
                                      "<br /> ICT4Events groep A"
                                      , gebruikersnaam, authenticatieCode);

            //Als de ontvanger geen html kan ontvangen word er een alternatief aangeboden.
            AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                (htmlBody, null, MediaTypeNames.Text.Html);

            //Een linked resoure maken om het plaatje aan de email te verbinden
            LinkedResource barcodeResource = new LinkedResource(bestandslocatie, MediaTypeNames.Image.Jpeg);
            barcodeResource.ContentId = "pictureBarcode";
            avHtml.LinkedResources.Add(barcodeResource);

            //In plaats van een body toe te voegen word er nu de alternatieve view toegevoegd.
            mail.AlternateViews.Add(avHtml);

            //emailadres van de afzender.
            mail.From = new MailAddress("ICT4EventsA@hotmail.com");

            //emailadres van de ontvanger.
            mail.To.Add(emailadres);
            
            //Onderwerp van het bericht.
            mail.Subject = "Verificatie e-mail";

            //Zorgt ervoor dat de html word verwerkt.
            mail.IsBodyHtml = true;

            //Poort van SMTP server.
            SmtpServer.Port = 587;
            
            //Inloggen op de SMTP server.
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ICT4EventsA@hotmail.com", "qwert12345");

            //Extra veiligheid.
            SmtpServer.EnableSsl = true;

            //Verzenden van de mail.
            SmtpServer.Send(mail);
        }

        //http://www.aspsnippets.com/Articles/Dynamically-Generate-and-Display-Barcode-Image-in-ASPNet.aspx
        public Image BarcodeGenereren(string barcodeString)
        {
            //Barcode word aangemaakt met de barcode van het polsbandje.
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode(barcodeString);

            //Het plaatje word zo verwerkt dat het makkelijk te scannen is.
            return barcode.Encode(TYPE.LOGMARS, barcodeString, Color.Black, Color.White, 500, 150);
        }

    }
}