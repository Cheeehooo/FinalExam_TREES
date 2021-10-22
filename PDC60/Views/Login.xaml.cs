using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDC60.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        iAuth auth;
        public Login()
        {
            InitializeComponent();
        }
        async void LoginClicked(object sender, EventArgs e)
        {
            try
            {
                if (EmailInput.Text != null)
                {
                    Application.Current.MainPage = new NavigationPage();

                }
            }
            catch (NullReferenceException ex)
            {
                await DisplayAlert("Authentication Failed", "Email or Password field are Empty", "OK");
            }

            string token = await auth.LoginWithEmailAndPassword(EmailInput.Text, Password.Text);
            if (token != string.Empty)
            {
                Application.Current.MainPage = new NavigationPage(new Views.ViewTrees());
            }

            else
            {
                await DisplayAlert("Authentication Failed", "Email or Password are Inccorect", "OK");

            }
        }

    }
}