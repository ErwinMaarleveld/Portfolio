using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rentalia.Data;
using Xamarin.Forms;
using PCLStorage;
using Rentalia.FourMistakesAPIClient;

namespace Rentalia
{

    public class FieldCheckerMethod : ContentPage
    {
        public bool IsFilled(string field)
        {
            try
            {
                if ((field.Length > 0) && field != ("Xamarin.Forms.Entry"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(System.NullReferenceException)
            {
                return false;
            }
            
        }
        public bool PassEqual(string pass1, string pass2)
        {
            if (pass1 == pass2)
            {
                return true;
            }
            else
            {
                DisplayAlert("Alert", "Je ingevulde wachtwoorden komen niet overeen!", "Oke");
                return false;
            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
       

        public void AddGebruiker(string voornaam, string tussen, string achternaam, string email, string pass1, string pass2)
        {
            if (IsFilled(email) && IsFilled(voornaam) && IsFilled(achternaam) && IsFilled(pass1) && PassEqual(pass1, pass2))
            {
                DateTime now = DateTime.Now;
                Gebruiker nieuweGebruiker = new Gebruiker("defaultGCode", voornaam, tussen, achternaam, email, now, 0, 0, pass1);
                GebruikerClient client = new GebruikerClient();
                var result = client.Post(nieuweGebruiker);
                if (result)
                {
                    DisplayAlert("Succes", "Gebruiker geregistreerd.", "Oke");
                }
                else
                {
                    DisplayAlert("Alert", "Gebruiker niet geregistreerd. Er is iets misgegaan.", "Oke");
                }
            }
            else if (!(IsFilled(email) && IsFilled(voornaam) && IsFilled(achternaam) && IsFilled(pass1) && IsFilled(pass2)))
            {
                DisplayAlert("Alert", "Je hebt niet alle verplichte velden ingevuld!", "Oke");
            }
            else if (!(PassEqual(pass1, pass2)))
            {
                DisplayAlert("Alert", "De wachtwoorden die je hebt ingevuld komen niet overeen.", "Oke");
            }
            else
            {
                DisplayAlert("Alert", "Error", "Ben ik lekker mee...");
            }
        }

        public void AddAanbieding(string title, string desc, float prijs, Gebruiker ingelogd)
        {
            string prijsje = prijs.ToString();
            try
            {
                if (IsFilled(prijsje) && IsFilled(title) && IsFilled(desc))
                {
                    Aanbieding nieuweAanbieding = new Aanbieding("defaultACode", title, desc, prijs, DateTime.Now, (Gebruiker)Xamarin.Forms.Application.Current.Properties["loggedIn"]);
                    AanbiedingClient client = new AanbiedingClient();
                    var result = client.Post(nieuweAanbieding);
                    if (result)
                    {
                        DisplayAlert("Succes", "Aanbieding aangemaakt", "Oke");
                    }
                    else
                    {
                        DisplayAlert("Alert", "Aanbieding niet aangemaakt. Er is iets fout gegaan.", "Oke");
                    }
                }
                else
                {
                    DisplayAlert("Alert", "Je hebt niet alle velden ingevoerd.", "Oke");
                }
            }
            catch(System.NullReferenceException)
            {
                DisplayAlert("Alert", "Je hebt niet alle velden ingevoerd.", "Oke");
            }
        }

        public void AddBericht(string tekst, Aanbieding onderwerp, Gebruiker ontvanger)
        {
            if (IsFilled(tekst))
            {
                Bericht nieuwBericht = new Bericht(ontvanger, (Gebruiker)Xamarin.Forms.Application.Current.Properties["loggedIn"], onderwerp, onderwerp.Gebruiker, DateTime.Now, tekst);
                BerichtClient client = new BerichtClient();
                var result = client.Post(nieuwBericht);
                if (result)
                {
                    DisplayAlert("Succes", "Bericht verstuurd", "Oke");
                }
                else
                {
                    DisplayAlert("Alert", "Bericht niet verstuurd. Er is iets misgegaan.", "Oke");
                }
            }
            else
            {
                DisplayAlert("Alert", "Je probeert een bericht zonder tekst te versturen.", "Oke");
            }
            
        }

        async public void AddFoto(string bestandsnaam, string titel, string beschrijving, Byte[] dataArray)
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync("Pictures", CreationCollisionOption.ReplaceExisting);

            await Storelocal.SaveImage(dataArray, bestandsnaam, folder);
            //nieuweFoto moet nog wel naar de database, bestandsnaam moet gegenereert worden.
        }
    }
}
