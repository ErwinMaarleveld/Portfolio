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
	public partial class HubPage : ContentPage
	{
		public HubPage ()
		{
            BindingContext = this;
			InitializeComponent ();
            //Aanbieding[] hubAanbiedingen = new Aanbieding[] { new Aanbieding("veld", "Grasmaaier HUREN", "Goede grasmaair, zgan", 69, DateTime.Now, new Gebruiker("veld", "veld", "veld", "veld", "veld", DateTime.Now, 0, 0)), new Aanbieding("veld", "Opblaas zwembadje", "Goede grasmaair, zgan", 35, DateTime.Now, new Gebruiker("veld", "veld", "veld", "veld", "veld", DateTime.Now, 0, 0)), new Aanbieding("veld", "Tesla model X", "Goede grasmaair, zgan", 420, DateTime.Now, new Gebruiker("veld", "veld", "veld", "veld", "veld", DateTime.Now, 0, 0)) };
            AanbiedingClient client = new AanbiedingClient();
            stackView.ItemsSource = client.Get().ToList().GetRange(0, 5).ToArray();
        }

        public void OnClickMailBox(object sender, EventArgs e)
        {
            App.Current.MainPage = new MessagePage();
        }

        public void OnCLickUserPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new UserPage();
        }

        public void OnCLickLogOut(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        public void OnClickOfferPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new OfferPage();
        }

        private void OnClickAddOffer(object sender, EventArgs e)
        {
            App.Current.MainPage = new AddOffer();
        }

        private void OnClickRequestPage(object sender, ItemTappedEventArgs e)
        {
            App.Current.MainPage = new RequestPage((Aanbieding)e.Item);
        }
    }
}