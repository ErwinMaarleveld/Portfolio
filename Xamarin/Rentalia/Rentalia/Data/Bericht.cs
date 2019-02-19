using System;
using System.Collections.Generic;
using System.Text;

namespace Rentalia.Data
{
    public class Bericht
    {
        public Gebruiker Ontvanger { get; private set; }
        public Gebruiker Verzender { get; private set; }
        public Aanbieding Onderwerp { get; private set; }
        public Gebruiker Verhuurder { get; private set; }
        public DateTime Datumtijd { get; private set; }
        public string BerichtText { get; private set; }

        public Bericht(Gebruiker ontvanger, Gebruiker verzender, Aanbieding onderwerp, Gebruiker verhuurder, DateTime datumtijd, string berichtText)
        {
            Ontvanger = ontvanger;
            Verzender = verzender;
            Onderwerp = onderwerp;
            Verhuurder = verhuurder;
            Datumtijd = datumtijd;
            BerichtText = berichtText;
        }
    }
}
