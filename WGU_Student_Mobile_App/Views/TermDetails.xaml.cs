using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.Views
{
    [QueryProperty(nameof(TermId), nameof(TermId))]
    public partial class TermDetails : ContentPage
    {
        public string TermId { get; set; }
        ITermService termService;
        public TermDetails()
        {
            InitializeComponent();
            termService = DependencyService.Get<ITermService>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(TermId, out var result);

            TermDetailsModel termDetails = termService.GetTermDetails(result);
            BindingContext = termDetails;
        }
    }
}