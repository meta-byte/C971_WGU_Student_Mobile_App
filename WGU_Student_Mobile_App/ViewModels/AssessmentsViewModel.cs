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
    public static class SelectedAssessment
    {
        public static Assessment selectedAssessment { get; set; }
    }

    class AssessmentsViewModel : BaseViewModel
    {
        public ObservableCollection<Assessment> AssessmentCollection { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Assessment> EditCommand { get; }
        public AsyncCommand<Assessment> DeleteCommand { get; }
        public AsyncCommand<Assessment> SelectedCommand { get; }
        public AsyncCommand<Assessment> DetailsCommand { get; }


        IAssessmentService assessmentService;
        public AssessmentsViewModel()
        {
            Title = "Assessments";
            AssessmentCollection = new ObservableCollection<Assessment>();
            Task.Run(async () => await Refresh());
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            DeleteCommand = new AsyncCommand<Assessment>(Delete);
            SelectedCommand = new AsyncCommand<Assessment>(Selected);
            DetailsCommand = new AsyncCommand<Assessment>(Details);
            EditCommand = new AsyncCommand<Assessment>(Edit);

            assessmentService = DependencyService.Get<IAssessmentService>();


        }


        async Task Refresh()
        {
            IsBusy = true;

            AssessmentCollection.Clear();

            IEnumerable<Assessment> assessments = assessmentService.GetAssessments();

            foreach (var assessment in assessments)
            {
                AssessmentCollection.Add(assessment);
            }

            await Task.Delay(2000);

            IsBusy = false;
        }

        async Task Add()
        {
            var route = nameof(AddAssessment);
            await Shell.Current.GoToAsync(route);

        }

        async Task Edit(Assessment assessment)
        {
            assessment = SelectedAssessment.selectedAssessment;

            if (assessment == null)
            {
                return;
            }

            var route = $"{nameof(EditAssessment)}?AssessmentId={assessment.Id}";
            await Shell.Current.GoToAsync(route);

        }

        async Task Selected(Assessment assessment)
        {
            if (assessment == null)
            {
                return;
            }

            SelectedAssessment.selectedAssessment = assessment;
            await Task.Delay(1000);

        }

        async Task Details(Assessment assessment)
        {
            assessment = SelectedAssessment.selectedAssessment;

            if (assessment == null)
            {
                return;
            }

            var result = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you wish to delete this item", "OK", "Cancel");
            if (!result)
            {
                return;
            }

            var route = $"{nameof(AssessmentDetails)}?AssessmentId={assessment.Id}";
            await Shell.Current.GoToAsync(route);
        }


        async Task Delete(Assessment assessment)
        {
            assessment = SelectedAssessment.selectedAssessment;

            if (assessment == null)
                return;

            assessmentService.DeleteAssessment(assessment.Id);
            await Refresh();
        }
    }
}
