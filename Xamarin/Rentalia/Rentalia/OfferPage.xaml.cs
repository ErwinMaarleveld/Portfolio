using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Rentalia.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rentalia.FourMistakesAPIClient;
using System.Windows.Input;

namespace Rentalia
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPage : ContentPage
    {

        public string Name { get; set; }
        public string Huurprijs { get; set; }
        public string FileName {get; set;}
        public string ACode { get; set;}
        public ICommand goToSingleOffer { get; set; }

        public OfferPage()
        {
            BindingContext = this;
            InitializeComponent();
            AanbiedingClient client = new AanbiedingClient();
            listView.ItemsSource = client.Get();
        }

        void OnImageTapped(object sender, EventArgs args)
        {
            var image = sender as Image;
            var viewCell = image.Parent.Parent as ViewCell;

            if (image.HeightRequest < 250)
            {
                image.HeightRequest = image.Height + 100;
                viewCell.ForceUpdateSize();
            }
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

        private void OnClickAddOffer(object sender, EventArgs e)
        {
            App.Current.MainPage = new AddOffer();
        }

        private void OnClickRequestPage(object sender, ItemTappedEventArgs e)
        {

            App.Current.MainPage = new RequestPage((Aanbieding)e.Item);
        }
        
    }

    
    };


    

