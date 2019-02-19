using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rentalia.Data;

namespace Rentalia
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReplyPage : ContentPage
	{
        private Gebruiker Ontvanger;
        private Aanbieding Onderwerp;
        private ContentPage From;

		public ReplyPage (Gebruiker ontvanger, Aanbieding onderwerp, ContentPage fromm)
		{
            BindingContext = this;
            Ontvanger = ontvanger;
            Onderwerp = onderwerp;
            From = fromm;
			InitializeComponent ();
		}

        public void OnClickMailBox()
        {
            App.Current.MainPage = new MessagePage();
        }
        public void OnClickOfferPage()
        {
            App.Current.MainPage = new OfferPage();
        }
        public void OnClickUserPage()
        {
            App.Current.MainPage = new UserPage();
        }
        public void OnClickHubPage()
        {
            App.Current.MainPage = new HubPage();
        }

        public void OnClickAddOffer()
        {
            App.Current.MainPage = new AddOffer();
        }

        private void OnClickSend(object sender, EventArgs e)
        {
            FieldCheckerMethod a = new FieldCheckerMethod();
            a.AddBericht(replyText.Text, Onderwerp, Ontvanger);
            App.Current.MainPage = From;
        }
    }
}