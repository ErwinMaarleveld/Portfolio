using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Rentalia.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rentalia.FourMistakesAPIClient;

namespace Rentalia
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPage : ContentPage
	{
        private Gebruiker Current;
		public UserPage (Gebruiker current = null)
		{
			InitializeComponent ();
            if (current == null)
            {
                Current = (Gebruiker)Xamarin.Forms.Application.Current.Properties["loggedIn"];
            }
            else
            {
                Current = current;
            }
            stackView.ItemsSource = new Gebruiker[] { Current };
        }

        public void OnClickMailBox(object sender, EventArgs e)
        {
            App.Current.MainPage = new MessagePage();
        }

        public void OnCLickUserPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new UserPage();
        }

        public void OnCLickHubPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new HubPage();
        }

        public void OnCLickOfferPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new OfferPage();
        }

        private void OnClickAddOffer(object sender, EventArgs e)
        {
            App.Current.MainPage = new AddOffer();
        }

        async void OnUpload(object sender, EventArgs e)
        {
            try
            {

                FileData filedata = await CrossFilePicker.Current.PickFile();
                var a = new FieldCheckerMethod();
                a.AddFoto(filedata.FileName, "Entry field Titel", "Entry field Beschrijving", filedata.DataArray);

                // the dataarray of the file will be found in filedata.DataArray 
                // file name will be found in filedata.FileName;
                //etc etc.


            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", $"Error: {ex}", "Oke");
                //ExceptionHandler.ShowException(ex.Message);
            }
        }

    }
}