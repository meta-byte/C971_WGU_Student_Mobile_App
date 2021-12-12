using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.ViewModels
{
    class AddInstructorsViewModel : BaseViewModel
    {
        string name;
        string phone;
        string email;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }



        public AsyncCommand SaveCommand { get; }
        IInstructorService instructorService;

        public AddInstructorsViewModel()
        {
            Title = "Add Instructor";
            SaveCommand = new AsyncCommand(Save);
            instructorService = DependencyService.Get<IInstructorService>();

        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            instructorService.AddInstructor(name, phone, email);

            await Shell.Current.GoToAsync("..");
        }

    }
}
