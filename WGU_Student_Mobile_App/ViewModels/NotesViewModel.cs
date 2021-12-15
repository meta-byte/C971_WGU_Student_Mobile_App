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
    public static class SelectedNote
    {
        public static Note selectedNote { get; set; }
    }

    class NotesViewModel : BaseViewModel
    {
        public ObservableCollection<Note> NoteCollection { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Note> EditCommand { get; }
        public AsyncCommand<Note> DeleteCommand { get; }
        public AsyncCommand<Note> SelectedCommand { get; }
        public AsyncCommand<Note> DetailsCommand { get; }


        INoteService noteService;
        public NotesViewModel()
        {
            Title = "Notes";
            NoteCollection = new ObservableCollection<Note>();
            Task.Run(async () => await Refresh());

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            DeleteCommand = new AsyncCommand<Note>(Delete);
            SelectedCommand = new AsyncCommand<Note>(Selected);
            DetailsCommand = new AsyncCommand<Note>(Details);
            EditCommand = new AsyncCommand<Note>(Edit);

            noteService = DependencyService.Get<INoteService>();

        }


        async Task Refresh()
        {
            IsBusy = true;

            NoteCollection.Clear();

            IEnumerable<Note> notes = noteService.GetNotes();

            foreach (var note in notes)
            {
                NoteCollection.Add(note);
            }

            await Task.Delay(2000);

            IsBusy = false;
        }

        async Task Add()
        {
            var route = nameof(AddNote);
            await Shell.Current.GoToAsync(route);

        }

        async Task Edit(Note note)
        {
            note = SelectedNote.selectedNote;

            if (note == null)
            {
                return;
            }

            var route = $"{nameof(EditNote)}?NoteId={note.Id}";
            await Shell.Current.GoToAsync(route);

        }

        async Task Selected(Note note)
        {
            if (note == null)
            {
                return;
            }

            SelectedNote.selectedNote = note;
            await Task.Delay(1000);

        }

        async Task Details(Note note)
        {
            note = SelectedNote.selectedNote;

            if (note == null)
            {
                return;
            }

            var route = $"{nameof(NoteDetails)}?NoteId={note.Id}";
            await Shell.Current.GoToAsync(route);
        }


        async Task Delete(Note note)
        {

            note = SelectedNote.selectedNote;

            if (note == null)
                return;

           var result = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you wish to delete this item", "OK", "Cancel");
            if (!result)
            {
                return;
            }

            noteService.DeleteNote(note.Id);
            await Refresh();
        }
    }
}
