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
    public static class SelectedCourse
    {
        public static Course selectedCourse { get; set; }
    }

    class CoursesViewModel : BaseViewModel
    {
        public ObservableCollection<Course> CourseCollection { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Course> EditCommand { get; }
        public AsyncCommand<Course> DeleteCommand { get; }
        public AsyncCommand<Course> SelectedCommand { get; }
        public AsyncCommand<Course> DetailsCommand { get; }


        ICourseService courseService;
        public CoursesViewModel()
        {
            Title = "Courses";
            CourseCollection = new ObservableCollection<Course>();
            Task.Run(async () => await Refresh());

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            DeleteCommand = new AsyncCommand<Course>(Delete);
            SelectedCommand = new AsyncCommand<Course>(Selected);
            DetailsCommand = new AsyncCommand<Course>(Details);
            EditCommand = new AsyncCommand<Course>(Edit);

            courseService = DependencyService.Get<ICourseService>();

        }


        async Task Refresh()
        {
            IsBusy = true;

            CourseCollection.Clear();

            IEnumerable<Course> courses = courseService.GetCourses();

            foreach (var course in courses)
            {
                CourseCollection.Add(course);
            }

            await Task.Delay(2000);

            IsBusy = false;
        }

        async Task Add()
        {
            var route = nameof(AddCourse);
            await Shell.Current.GoToAsync(route);
        }


        async Task Edit(Course course)
        {
            course = SelectedCourse.selectedCourse;

            if (course == null)
            {
                return;
            }

            var route = $"{nameof(EditCourse)}?CourseId={course.Id}";
            await Shell.Current.GoToAsync(route);

        }

        async Task Selected(Course course)
        {
            if (course == null)
            {
                return;
            }

            SelectedCourse.selectedCourse = course;
            await Task.Delay(1000);

        }

        async Task Details(Course course)
        {
            course = SelectedCourse.selectedCourse;

            if (course == null)
            {
                return;
            }

            var route = $"{nameof(CourseDetails)}?CourseId={course.Id}";
            await Shell.Current.GoToAsync(route);
        }


        async Task Delete(Course course)
        {
            course = SelectedCourse.selectedCourse;

            if (course == null)
                return;

            var result = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you wish to delete this item", "OK", "Cancel");
            if (!result)
            {
                return;
            }

            courseService.DeleteCourse(course.Id);
            await Refresh();
        }
    }
}
