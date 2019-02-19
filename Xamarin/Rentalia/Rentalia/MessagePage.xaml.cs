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
	public partial class MessagePage : ContentPage
	{
		public MessagePage ()
		{
            BindingContext = this;
			InitializeComponent ();
            BerichtClient client = new BerichtClient();
            //Bericht[] alleBerichten = new Bericht[] { new Bericht(new Gebruiker("0005", "Bart", "van", "Velden", "BartvVelden@gmail.com", DateTime.Now, 0, 0), new Gebruiker("0009", "Harry", "", "Hertog", "Harry@Hertog.nl", DateTime.Now, 0, 0), new Aanbieding("veld", "Grasmaaier HUREN", "Goede grasmaair, zgan", 69, DateTime.Now, new Gebruiker("veld", "veld", "veld", "veld", "veld", DateTime.Now, 0, 0)), new Gebruiker("0005", "Bart", "van", "Velden", "BartvVelden@gmail.com", DateTime.Now, 0, 0), DateTime.Now, "Intresse in grasmaaier") };
            Gebruiker loggedIn = (Gebruiker)Xamarin.Forms.Application.Current.Properties["loggedIn"];
            Bericht[] alleBerichten = client.Get(loggedIn.GCode);
            stackView.ItemsSource = alleBerichten;
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

        public void OnClickMessage(object sender, ItemTappedEventArgs e)
        {
            App.Current.MainPage = new MessageView((Bericht)e.Item);
        }
    }
}