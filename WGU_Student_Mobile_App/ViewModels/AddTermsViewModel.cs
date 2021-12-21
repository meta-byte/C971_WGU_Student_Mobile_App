using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.ViewModels
{
    public class AddTermsViewModel : BaseViewModel
    {
        string name;
        DateTime startDate = DateTime.Now.Date;
        DateTime endDate = DateTime.Now.Date.AddMonths(6);
        bool hasNotified;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public DateTime StartDate { get => startDate; set => SetProperty(ref startDate, value); }
        public DateTime EndDate { get => endDate; set => SetProperty(ref endDate, value); }


        public AsyncCommand SaveCommand { get; }
        ITermService termService;

        public AddTermsViewModel()
        {
            Title = "Add Term";

            SaveCommand = new AsyncCommand(Save);
            termService = DependencyService.Get<ITermService>();

        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                await Application.Current.MainPage.DisplayAlert("Empty Fields", "All fields are required", "OK");
                return;
            }

            termService.AddTerm(name, startDate, endDate, hasNotified);

            await Shell.Current.GoToAsync("..");
        }
    }
}
