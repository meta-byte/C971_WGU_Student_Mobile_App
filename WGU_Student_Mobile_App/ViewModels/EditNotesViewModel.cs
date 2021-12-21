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
    [QueryProperty(nameof(NoteId), nameof(NoteId))]
    class EditNotesViewModel : BaseViewModel
    {
        public ObservableCollection<Course> CourseCollection { get; set; }

        int id;
        public string NoteId { get; set; }
        int courseId;
        string name;
        string description;
        DateTime createdDate = DateTime.Now.Date;

        public int CourseId { get => courseId; set => SetProperty(ref courseId, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public DateTime CreatedDate { get => createdDate; set => SetProperty(ref createdDate, value); }


        public AsyncCommand SaveCommand { get; }
        public AsyncCommand LoadCommand { get; }



        INoteService noteService;
        ICourseService courseService;

        public EditNotesViewModel()
        {
            Title = "Add Note";
            CourseCollection = new ObservableCollection<Course>();

            SaveCommand = new AsyncCommand(Save);
            LoadCommand = new AsyncCommand(Load);
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

        async Task Load()
        {
            IsBusy = true;

            await GetCourses();

            id = int.Parse(NoteId);
            Note note = noteService.GetNote(id);
            Name = note.Name;
            Description = note.Description;
            CreatedDate = note.CreatedDate;

            IsBusy = false;

            await Task.Delay(1000);
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                await Application.Current.MainPage.DisplayAlert("Empty Fields", "All fields are required", "OK");
                return;
            }

            courseId = courseId + 1;

            noteService.EditNote(id, courseId, name, description, createdDate);

            await Shell.Current.GoToAsync("..");
        }

    }
}
