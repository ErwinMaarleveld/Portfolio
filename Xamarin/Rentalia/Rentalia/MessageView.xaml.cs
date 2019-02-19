using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rentalia.Data;
using Rentalia.FourMistakesAPIClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rentalia
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessageView : ContentPage
	{
        private Bericht CurrentBericht;

		public MessageView (Bericht bericht)
		{
            BindingContext = this;
			InitializeComponent ();
            CurrentBericht = bericht;
            var a = (Gebruiker)Xamarin.Forms.Application.Current.Properties["loggedIn"];
            Bericht[] berichten = new Bericht[] { CurrentBericht };
            stackView.ItemsSource = berichten;
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

        public void OnClickReply(object sender, EventArgs e)
        {
            App.Current.MainPage = new ReplyPage(CurrentBericht.Verzender, CurrentBericht.Onderwerp, this);
        }
    }
}