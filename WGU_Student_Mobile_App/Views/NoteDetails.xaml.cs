using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.Views
{
    [QueryProperty(nameof(NoteId), nameof(NoteId))]
    public partial class NoteDetails : ContentPage
    {
        public string NoteId { get; set; }
        INoteService noteService;
        public NoteDetails()
        {
            InitializeComponent();
            noteService = DependencyService.Get<INoteService>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(NoteId, out var result);

            NoteDetailsModel noteDetails = noteService.GetNoteDetails(result);
            BindingContext = noteDetails;

        }
    }
}