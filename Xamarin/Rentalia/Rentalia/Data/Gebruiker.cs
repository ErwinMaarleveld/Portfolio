using System;
using System.Collections.Generic;
using System.Text;

namespace Rentalia.Data
{
    public class Gebruiker
    {
        public string GCode { get; private set; }
        public string Voornaam { get; protected set; }
        public string Tussenvoegsel { get; protected set; }
        public string Achternaam { get; protected set; }
        public string Email { get; protected set; }
        public DateTime LidGeworden { get; protected set; }
        public float Rating { get; protected set; }
        public int AantalRatings { get; protected set; }
        public string Wachtwoord { get; private set; }

        public Gebruiker(string gCode, string voornaam, string tussenvoegsel, string achternaam, string email, DateTime lidGeworden, float rating, int aantalRatings, string wachtwoord = null)
        {
            GCode = gCode;
            Voornaam = voornaam;
            Tussenvoegsel = tussenvoegsel;
            Achternaam = achternaam;
            Email = email;
            LidGeworden = lidGeworden;
            Rating = rating;
            AantalRatings = aantalRatings;
            if (wachtwoord == null)
            {
                Wachtwoord = "";
            }
            else
            {
                Wachtwoord = wachtwoord;
            }
        }
    }
}
