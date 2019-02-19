using System;
using System.Collections.Generic;
using System.Text;

namespace Rentalia.Data
{
    public class Aanbieding
    {
        public string ACode { get; private set; }
        public string Titel { get; protected set; }
        public string Beschrijving { get; protected set; }
        public float Huurprijs { get; protected set; }
        public DateTime Geplaatsd { get; protected set; }
        public Gebruiker Gebruiker { get; protected set; }

        public Aanbieding(string aCode, string titel, string beschrijving, float huurprijs, DateTime geplaatsd, Gebruiker gebruiker)
        {
            ACode = aCode;
            Titel = titel;
            Beschrijving = beschrijving;
            Huurprijs = huurprijs;
            Geplaatsd = geplaatsd;
            Gebruiker = gebruiker;
        }
    }
}
