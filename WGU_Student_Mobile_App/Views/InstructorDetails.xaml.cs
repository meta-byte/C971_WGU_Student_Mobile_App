using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.Views
{
    [QueryProperty(nameof(InstructorId), nameof(InstructorId))]
    public partial class InstructorDetails : ContentPage
    {
        public string InstructorId { get; set; }
        IInstructorService assessmentService;
        public InstructorDetails()
        {
            InitializeComponent();
            assessmentService = DependencyService.Get<IInstructorService>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(InstructorId, out var result);

            BindingContext = assessmentService.GetInstructor(result);
        }

    }
}