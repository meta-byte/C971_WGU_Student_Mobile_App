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
    class AddNotesViewModel : BaseViewModel
    {
        public ObservableCollection<Course> CourseCollection { get; set; }

        int courseId;
        string name;
        string description;
        DateTime createdDate = DateTime.Now.Date;

        public int CourseId { get => courseId; set => SetProperty(ref courseId, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public DateTime CreatedDate { get => createdDate; set => SetProperty(ref createdDate, value); }


        public AsyncCommand SaveCommand { get; }
        public AsyncCommand GetCommand { get; }
        INoteService noteService;
        ICourseService courseService;


        public AddNotesViewModel()
        {
            Title = "Add Note";

            CourseCollection = new ObservableCollection<Course>();

            SaveCommand = new AsyncCommand(Save);
            GetCommand = new AsyncCommand(GetCourses);

            noteService = DependencyService.Get<INoteService>();
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

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                await Application.Current.MainPage.DisplayAlert("Empty Fields", "All fields are required", "OK");
                return;
            }

            courseId = courseId + 1;

            noteService.AddNote(courseId, name, description, createdDate);

            await Shell.Current.GoToAsync("..");
        }

    }
}
