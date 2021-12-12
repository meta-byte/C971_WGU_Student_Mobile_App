using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Threading;
using WGU_Student_Mobile_App.Models;
using Xamarin.Forms;

namespace WGU_Student_Mobile_App.Services
{
    public class NotificationService
    {
        ICourseService courseService;
        ITermService termService;
        IAssessmentService assessmentService;

        public void Notify()
        {
            courseService = DependencyService.Get<ICourseService>();
            termService = DependencyService.Get<ITermService>();
            assessmentService = DependencyService.Get<IAssessmentService>();


            var loop = true;
            while (loop)
            {

                Thread.Sleep(10000);
                var i = 1;

                IEnumerable<Term> terms = termService.GetTerms();
                foreach (var term in terms)
                {
                    if (term.HasNotified == false & term.StartDate.Date < DateTime.Now.Date.AddDays(+5))
                    {
                        CrossLocalNotifications.Current.Show(term.Name, "Your term is starting soon!", i);

                        var hasNotified = true;
                        i++;
                        termService.EditTerm(term.Id, term.Name, term.StartDate, term.EndDate, hasNotified);
                    }
                }

                IEnumerable<Course> courses = courseService.GetCourses();
                foreach (var course in courses)
                {
                    if (course.HasNotified == false & course.StartDate.Date < DateTime.Now.Date.AddDays(+5))
                    {
                        CrossLocalNotifications.Current.Show(course.Name, "Your course is starting soon!", i);

                        var hasNotified = true;
                        i++;
                        courseService.EditCourse(course.TermId, course.Id, course.Name, course.StartDate, course.EndDate, course.InstructorId, course.CourseStatus, hasNotified);
                    }
                }

                IEnumerable<Assessment> assessments = assessmentService.GetAssessments();
                foreach (var assessment in assessments)
                {
                    if (assessment.HasNotified == false & assessment.StartDate.Date < DateTime.Now.Date.AddDays(+5))
                    {
                        CrossLocalNotifications.Current.Show(assessment.Name, "Your assessment is starting soon!", i);

                        var hasNotified = true;
                        i++;
                        assessmentService.EditAssessment(assessment.Id, assessment.CourseId, assessment.Name, assessment.StartDate, assessment.EndDate, assessment.AssessmentType, hasNotified);
                    }
                }


            }

        }
    }
}
