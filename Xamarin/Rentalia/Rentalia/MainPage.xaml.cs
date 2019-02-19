using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace Rentalia
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
        }



        public void OnClickRegister(object sender, EventArgs e)
        {
            App.Current.MainPage = new RegisterPage();
        }

        public void OnClickHub(object sender, EventArgs e)
        {//Sign-in / login brings you here
            var b = new Login();
            var a = new FieldCheckerMethod();
            if (b.CheckCredentials(email.Text, pass.Text) && a.IsValidEmail(email.Text))
            {
                App.Current.MainPage = new HubPage();
            }
            if (! a.IsValidEmail(email.Text))
            {
                DisplayAlert("Alert", "Vul een geldige e-mail in.", "Oke");
            }
            if (! b.CheckCredentials(email.Text, pass.Text))
            {
                DisplayAlert("Alert", "Email en/of wachtwoord komen niet overeen.", "Oke");
            }
        }
        

    }
}
