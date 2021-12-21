using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using WGU_Student_Mobile_App.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;

namespace WGU_Student_Mobile_App.ViewModels
{
    class ReportsViewModel : BaseViewModel
    {
        Object reportType;
        public Object ReportType { get => reportType; set => SetProperty(ref reportType, value); }


        ICourseService courseService;
        ITermService termService;

        public AsyncCommand GenerateCommand { get; }

        public ReportsViewModel()
        {
            courseService = DependencyService.Get<ICourseService>();
            termService = DependencyService.Get<ITermService>();

            GenerateCommand = new AsyncCommand(Generate);


        }
 
        async Task Generate()
        {
            if (reportType.ToString() == "Planner Overview")
            {
               await GenerateOverview();
            }
        }

        async Task GenerateOverview()
        {
            string timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            List<string> lines = new List<string>();

            lines.Add("Planner Overview");
            lines.Add("");
            lines.Add("Generated: " + DateTime.Now.ToString());
            lines.Add("");
            lines.Add("-------------------------");
            lines.Add("");


            List<Term> terms = termService.GetTerms();
            foreach(Term t in terms)
            {
                lines.Add(t.Name);
                TermDetailsModel td = termService.GetTermDetails(t.Id);
                foreach(Course c in td.Courses)
                {
                    lines.Add("   - " + c.Name);
                    CourseDetailsModel cd = courseService.GetCourseDetails(c.Id);
                    foreach(Assessment a in cd.Assessments)
                    {
                        lines.Add("       - " + a.Name);
                    }

                }
            }
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "Report_" + timestamp + ".txt");
            File.WriteAllLines(filePath, lines);
            await Task.Delay(500);
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            }) ;
        }
    }
}
