using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.Views
{
    [QueryProperty(nameof(AssessmentId), nameof(AssessmentId))]
    public partial class AssessmentDetails : ContentPage
    {
        public string AssessmentId { get; set; }
        IAssessmentService assessmentService;
        public AssessmentDetails()
        {
            InitializeComponent();
            assessmentService = DependencyService.Get<IAssessmentService>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(AssessmentId, out var result);

            AssessmentDetailsModel assessmentDetails = assessmentService.GetAssessmentDetails(result);
            BindingContext = assessmentDetails;
        }
    }
}