using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGU_Student_Mobile_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Terms)}");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Registration)}");
        }
    }
}