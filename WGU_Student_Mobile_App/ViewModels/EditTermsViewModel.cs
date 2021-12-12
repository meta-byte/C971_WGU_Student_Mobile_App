using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.ViewModels
{
    [QueryProperty(nameof(TermId), nameof(TermId))]
    class EditTermsViewModel : BaseViewModel
    {
        int id;
        public string TermId { get; set; }
        string name;
        DateTime startDate = DateTime.Now.Date;
        DateTime endDate = DateTime.Now.Date.AddMonths(6);
        bool hasNotified;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public DateTime StartDate { get => startDate; set => SetProperty(ref startDate, value); }
        public DateTime EndDate { get => endDate; set => SetProperty(ref endDate, value); }


        public AsyncCommand SaveCommand { get; }
        public AsyncCommand LoadCommand { get; }

        ITermService termService;

        public EditTermsViewModel()
        {
            Title = "Edit Term";

            SaveCommand = new AsyncCommand(Save);
            LoadCommand = new AsyncCommand(Load);

            termService = DependencyService.Get<ITermService>();

        }

        async Task Load()
        {
            IsBusy = true;

            id = int.Parse(TermId);
            Term term = termService.GetTerm(id);
            Name = term.Name;
            StartDate = term.StartDate;
            EndDate = term.EndDate;

            IsBusy = false;

            await Task.Delay(1000);
        }

        async Task Save()
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            termService.EditTerm(id, name, startDate, endDate, hasNotified);

            await Shell.Current.GoToAsync("..");
        }

    }
}
