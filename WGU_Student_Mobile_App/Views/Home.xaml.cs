using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGU_Student_Mobile_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}