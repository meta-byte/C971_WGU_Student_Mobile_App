using SQLite;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            SQLiteConnection db = new DatabaseService().InitializeDatabase();
            DependencyService.RegisterSingleton<ICourseService>(new CourseService(db));
            DependencyService.RegisterSingleton<IAssessmentService>(new AssessmentService(db));
            DependencyService.RegisterSingleton<ITermService>(new TermService(db));
            DependencyService.RegisterSingleton<INoteService>(new NoteService(db));
            DependencyService.RegisterSingleton<IInstructorService>(new InstructorService(db));
            DependencyService.RegisterSingleton<IInstructorService>(new InstructorService(db));

            NotificationService notification = new NotificationService();
            Task.Run(notification.Notify);

            //System.Environment.Exit(0);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


    }
}
