using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.ViewModels
{
    [QueryProperty(nameof(InstructorId), nameof(InstructorId))]
    class EditInstructorsViewModel : BaseViewModel
    {
        int id;
        public string InstructorId { get; set; }

        string name;
        string phone;
        string email;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }



        public AsyncCommand SaveCommand { get; }
        public AsyncCommand LoadCommand { get; }

        IInstructorService instructorService;

        public EditInstructorsViewModel()
        {
            Title = "Add Instructor";
            SaveCommand = new AsyncCommand(Save);
            LoadCommand = new AsyncCommand(Load);

            instructorService = DependencyService.Get<IInstructorService>();

        }

        async Task Load()
        {
            IsBusy = true;

            id = int.Parse(InstructorId);
            Instructor instructor = instructorService.GetInstructor(id);
            Name = instructor.Name;
            Phone = instructor.PhoneNumber;
            Email = instructor.Email;

            IsBusy = false;

            await Task.Delay(500);
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            instructorService.EditInstructor(id, name, phone, email);

            await Shell.Current.GoToAsync("..");
        }

    }
}
