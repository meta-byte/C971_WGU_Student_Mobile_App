using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.ViewModels
{
    [QueryProperty(nameof(AssessmentId), nameof(AssessmentId))]
    class EditAssessmentsViewModel : BaseViewModel
    {
        public ObservableCollection<Course> CourseCollection { get; set; }


        int id;
        public string AssessmentId { get; set; }

        int courseId;
        string name;
        DateTime startDate = DateTime.Now.Date;
        DateTime endDate = DateTime.Now.Date.AddDays(14);
        AssessmentTypes assessmentType = AssessmentTypes.NA;
        bool hasNotified;

        public int CourseId { get => courseId; set => SetProperty(ref courseId, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public DateTime StartDate { get => startDate; set => SetProperty(ref startDate, value); }
        public DateTime EndDate { get => endDate; set => SetProperty(ref endDate, value); }
        public string[] Types { get; } = Enum.GetNames(typeof(AssessmentTypes));
        public AssessmentTypes AssessmentType { get => assessmentType; set => SetProperty(ref assessmentType, value); }


        public AsyncCommand SaveCommand { get; }
        public AsyncCommand LoadCommand { get; }

        IAssessmentService assessmentService;
        ICourseService courseService;

        public EditAssessmentsViewModel()
        {
            Title = "Add Assessment";

            CourseCollection = new ObservableCollection<Course>();

            SaveCommand = new AsyncCommand(Save);
            LoadCommand = new AsyncCommand(Load);

            assessmentService = DependencyService.Get<IAssessmentService>();
            courseService = DependencyService.Get<ICourseService>();

        }

        async Task GetCourses()
        {
            IsBusy = true;

            IEnumerable<Course> courses = courseService.GetCourses();
            foreach (var course in courses)
            {
                CourseCollection.Add(course);
            }

            await Task.Delay(1000);

            IsBusy = false;
        }

        async Task Load()
        {
            IsBusy = true;

            await GetCourses();

            id = int.Parse(AssessmentId);
            Assessment assessment = assessmentService.GetAssessment(id);
            Name = assessment.Name;
            CourseId = assessment.CourseId;
            StartDate = assessment.StartDate;
            EndDate = assessment.EndDate;
            AssessmentType = assessment.AssessmentType;

            IsBusy = false;

            await Task.Delay(500);
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                await Application.Current.MainPage.DisplayAlert("Empty Fields", "All fields are required", "OK");
                return;
            }

            courseId = courseId + 1;

            assessmentService.EditAssessment(id, courseId, name, startDate, endDate, assessmentType, hasNotified);

            await Shell.Current.GoToAsync("..");
        }

    }
}
