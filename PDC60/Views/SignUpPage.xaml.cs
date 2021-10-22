using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PDC60.Serivces;

namespace PDC60.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        iAuth auth;
        public SignUpPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<iAuth>();
        }

        async void SignUpClicked(object sender, EventArgs e)
        {
            var user = auth.SignUpWithEmailAndPassword(EmailInput.Text, Password.Text);

            if (user!=null)
            {
                await DisplayAlert("Success", "New user has been created! ", "OK");
                var signOut = auth.SignOut();
                if (signOut)
                {
                    Application.Current.MainPage = new Home();
                }
            }
            else
            {
                await DisplayAlert("Error", "Something went wrong", "OK");
            }
        }
    }
}