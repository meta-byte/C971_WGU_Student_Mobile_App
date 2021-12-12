using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.Views
{
    [QueryProperty(nameof(CourseId), nameof(CourseId))]
    public partial class CourseDetails : ContentPage
    {
        public string CourseId { get; set; }
        ICourseService courseService;
        public CourseDetails()
        {
            InitializeComponent();
            courseService = DependencyService.Get<ICourseService>();
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            int.TryParse(CourseId, out var result);

            CourseDetailsModel courseDetails = courseService.GetCourseDetails(result);
            BindingContext = courseDetails;

        }
    }
}