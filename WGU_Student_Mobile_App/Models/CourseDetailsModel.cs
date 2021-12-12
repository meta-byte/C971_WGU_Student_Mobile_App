using System;
using System.Collections.Generic;

namespace WGU_Student_Mobile_App.Models
{
    public class CourseDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TermName { get; set; }

        public string InstructorName { get; set; }

        public CourseStatuses CourseStatus { get; set; }

        public List<Assessment> Assessments { get; set; }

        public List<Note> Notes { get; set; }

        public CourseDetailsModel()
        {

        }
    }
}
