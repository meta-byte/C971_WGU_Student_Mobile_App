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
    public static class SelectedInstructor
    {
        public static Instructor selectedInstructor { get; set; }
    }

    class InstructorsViewModel : BaseViewModel
    {
        public ObservableCollection<Instructor> InstructorCollection { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Instructor> EditCommand { get; }
        public AsyncCommand<Instructor> DeleteCommand { get; }
        public AsyncCommand<Instructor> SelectedCommand { get; }
        public AsyncCommand<Instructor> DetailsCommand { get; }


        IInstructorService instructorService;
        public InstructorsViewModel()
        {
            Title = "Instructors";
            InstructorCollection = new ObservableCollection<Instructor>();
            Task.Run(async () => await Refresh());
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            DeleteCommand = new AsyncCommand<Instructor>(Delete);
            SelectedCommand = new AsyncCommand<Instructor>(Selected);
            DetailsCommand = new AsyncCommand<Instructor>(Details);
            EditCommand = new AsyncCommand<Instructor>(Edit);

            instructorService = DependencyService.Get<IInstructorService>();


        }


        async Task Refresh()
        {
            IsBusy = true;

            InstructorCollection.Clear();

            IEnumerable<Instructor> instructors = instructorService.GetInstructors();

            foreach (var instructor in instructors)
            {
                InstructorCollection.Add(instructor);
            }

            await Task.Delay(2000);

            IsBusy = false;
        }

        async Task Add()
        {
            var route = nameof(AddInstructor);
            await Shell.Current.GoToAsync(route);

        }

        async Task Edit(Instructor instructor)
        {
            instructor = SelectedInstructor.selectedInstructor;

            if (instructor == null)
            {
                return;
            }

            var route = $"{nameof(EditInstructor)}?InstructorId={instructor.Id}";
            await Shell.Current.GoToAsync(route);

        }

        async Task Selected(Instructor instructor)
        {
            if (instructor == null)
            {
                return;
            }

            SelectedInstructor.selectedInstructor = instructor;
            await Task.Delay(1000);

        }

        async Task Details(Instructor instructor)
        {
            instructor = SelectedInstructor.selectedInstructor;

            if (instructor == null)
            {
                return;
            }

            var route = $"{nameof(InstructorDetails)}?InstructorId={instructor.Id}";
            await Shell.Current.GoToAsync(route);
        }


        async Task Delete(Instructor instructor)
        {
            instructor = SelectedInstructor.selectedInstructor;

            if (instructor == null)
                return;

            var result = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you wish to delete this item", "OK", "Cancel");
            if (!result)
            {
                return;
            }

            instructorService.DeleteInstructor(instructor.Id);
            await Refresh();
        }

    }
}
