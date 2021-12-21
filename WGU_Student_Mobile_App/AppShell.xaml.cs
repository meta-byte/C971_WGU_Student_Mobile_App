using WGU_Student_Mobile_App.Views;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();


            //Home, Login, Registration
            Routing.RegisterRoute(nameof(Home),
                typeof(Home));

            Routing.RegisterRoute(nameof(Login),
                typeof(Login));

            Routing.RegisterRoute(nameof(Registration),
                typeof(Registration));

            Routing.RegisterRoute(nameof(Search),
                typeof(Search));

            Routing.RegisterRoute(nameof(Reports),
                typeof(Reports));

            //Terms
            Routing.RegisterRoute(nameof(Terms),
                typeof(Terms));

            Routing.RegisterRoute(nameof(AddTerm),
                typeof(AddTerm));

            Routing.RegisterRoute(nameof(EditTerm),
                typeof(EditTerm));

            Routing.RegisterRoute(nameof(TermDetails),
                typeof(TermDetails));


            //Courses
            Routing.RegisterRoute(nameof(Courses),
                typeof(Courses));

            Routing.RegisterRoute(nameof(AddCourse),
                typeof(AddCourse));

            Routing.RegisterRoute(nameof(EditCourse),
                typeof(EditCourse));

            Routing.RegisterRoute(nameof(CourseDetails),
                typeof(CourseDetails));


            //Assessments
            Routing.RegisterRoute(nameof(Assessments),
                typeof(Assessments));

            Routing.RegisterRoute(nameof(AddAssessment),
                typeof(AddAssessment));

            Routing.RegisterRoute(nameof(EditAssessment),
                typeof(EditAssessment));

            Routing.RegisterRoute(nameof(AssessmentDetails),
                typeof(AssessmentDetails));


            //Notes
            Routing.RegisterRoute(nameof(Notes),
                typeof(Notes));

            Routing.RegisterRoute(nameof(AddNote),
                typeof(AddNote));

            Routing.RegisterRoute(nameof(EditNote),
                typeof(EditNote));

            Routing.RegisterRoute(nameof(NoteDetails),
                typeof(NoteDetails));


            //Instructors
            Routing.RegisterRoute(nameof(Instructors),
                typeof(Instructors));

            Routing.RegisterRoute(nameof(AddInstructor),
                typeof(AddInstructor));

            Routing.RegisterRoute(nameof(EditInstructor),
                typeof(EditInstructor));

            Routing.RegisterRoute(nameof(InstructorDetails),
                typeof(InstructorDetails));
        }

    }
}
