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
    [QueryProperty(nameof(CourseId), nameof(CourseId))]
    class EditCoursesViewModel : BaseViewModel
    {
        public ObservableCollection<Term> TermCollection { get; set; }
        public ObservableCollection<Instructor> InstructorCollection { get; set; }

        int id;
        public string CourseId { get; set; }

        int termId;
        int instructorId;
        string name;
        DateTime startDate = DateTime.Now.Date;
        DateTime endDate = DateTime.Now.Date.AddMonths(6);
        CourseStatuses status = CourseStatuses.NA;
        bool hasNotified;

        public int TermID { get => termId; set => SetProperty(ref termId, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public DateTime StartDate { get => startDate; set => SetProperty(ref startDate, value); }
        public DateTime EndDate { get => endDate; set => SetProperty(ref endDate, value); }
        public int InstructorId { get => instructorId; set => SetProperty(ref instructorId, value); }
        public string[] Statuses { get; } = Enum.GetNames(typeof(CourseStatuses));
        public CourseStatuses Status { get => status; set => SetProperty(ref status, value); }

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand LoadCommand { get; }


        ICourseService courseService;
        ITermService termService;
        IInstructorService instructorService;

        public EditCoursesViewModel()
        {
            Title = "Add Course";

            TermCollection = new ObservableCollection<Term>();
            InstructorCollection = new ObservableCollection<Instructor>();

            SaveCommand = new AsyncCommand(Save);
            LoadCommand = new AsyncCommand(Load);


            courseService = DependencyService.Get<ICourseService>();
            termService = DependencyService.Get<ITermService>();
            instructorService = DependencyService.Get<IInstructorService>();

        }

        async Task Get()
        {
            IsBusy = true;

            IEnumerable<Term> terms = termService.GetTerms();
            foreach (var term in terms)
            {
                TermCollection.Add(term);
            }

            IEnumerable<Instructor> instructors = instructorService.GetInstructors();
            foreach (var instructor in instructors)
            {
                InstructorCollection.Add(instructor);
            }


            await Task.Delay(1000);

            IsBusy = false;
        }

        async Task Load()
        {
            IsBusy = true;

            await Get();

            id = int.Parse(CourseId);
            Course course = courseService.GetCourse(id);
            Name = course.Name;
            StartDate = course.StartDate;
            EndDate = course.EndDate;
            Status = course.CourseStatus;




            IsBusy = false;

            await Task.Delay(500);
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            //compensate for observable collection off by one

            termId = termId + 1;
            instructorId = instructorId + 1;

            courseService.EditCourse(termId, id, name, startDate, endDate, instructorId, status, hasNotified);

            await Shell.Current.GoToAsync("..");
        }
    }
}
