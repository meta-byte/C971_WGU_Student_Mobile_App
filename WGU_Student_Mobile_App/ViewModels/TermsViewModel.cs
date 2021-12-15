using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using WGU_Student_Mobile_App.Views;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.ViewModels
{
    public static class SelectedTerm
    {
        public static Term selectedTerm { get; set; }
    }

    public class TermsViewModel : BaseViewModel
    {
        public ObservableCollection<Term> TermCollection { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Term> EditCommand { get; }
        public AsyncCommand<Term> DeleteCommand { get; }
        public AsyncCommand<Term> SelectedCommand { get; }
        public AsyncCommand<Term> DetailsCommand { get; }


        ITermService termService;
        public TermsViewModel()
        {
            Title = "Terms";
            TermCollection = new ObservableCollection<Term>();
            Task.Run(async () => await Refresh());

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            DeleteCommand = new AsyncCommand<Term>(Delete);
            SelectedCommand = new AsyncCommand<Term>(Selected);
            DetailsCommand = new AsyncCommand<Term>(Details);
            EditCommand = new AsyncCommand<Term>(Edit);

            termService = DependencyService.Get<ITermService>();

        }

        async Task Refresh()
        {
            IsBusy = true;

            TermCollection.Clear();

            IEnumerable<Term> terms = termService.GetTerms();

            foreach (var term in terms)
            {
                TermCollection.Add(term);

            }

            await Task.Delay(2000);

            IsBusy = false;
        }

        async Task Add()
        {
            var route = nameof(AddTerm);
            await Shell.Current.GoToAsync(route);
        }

        async Task Edit(Term term)
        {

            term = SelectedTerm.selectedTerm;

            if (term == null)
            {
                return;
            }

            var route = $"{nameof(EditTerm)}?TermId={term.Id}";
            await Shell.Current.GoToAsync(route);

        }

        async Task Selected(Term term)
        {
            if (term == null)
            {
                return;
            }

            SelectedTerm.selectedTerm = term;
            await Task.Delay(1000);

        }

        async Task Details(Term term)
        {
            term = SelectedTerm.selectedTerm;

            if (term == null)
            {
                return;
            }

            var route = $"{nameof(TermDetails)}?TermId={term.Id}";
            await Shell.Current.GoToAsync(route);
        }


        async Task Delete(Term term)
        {
            term = SelectedTerm.selectedTerm;

            if (term == null)
                return;

            var result = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you wish to delete this item", "OK", "Cancel");
            if (!result)
            {
                return;
            }

            termService.DeleteTerm(term.Id);
            await Refresh();
        }
    }
}
