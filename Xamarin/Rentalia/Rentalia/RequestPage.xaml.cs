using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rentalia.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rentalia.FourMistakesAPIClient;

namespace Rentalia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestPage : ContentPage
    {
        private Aanbieding Current;

		public RequestPage (Aanbieding current)
		{
            BindingContext = this;
			InitializeComponent ();
            Current = current;
            Aanbieding[] aanbiedingen = new Aanbieding[] { Current };
            stackView.ItemsSource = aanbiedingen;
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

        public void OnClickUserPageOwner()
        {
            App.Current.MainPage = new UserPage(Current.Gebruiker);
        }

        public void OnClickHubPage()
        {
            App.Current.MainPage = new HubPage();
        }

        public void OnClickAddOffer()
        {
            App.Current.MainPage = new AddOffer();
        }

        public void OnClickSentRequest()
        {
            App.Current.MainPage = new ReplyPage(Current.Gebruiker, Current, this);
        }

    }
}