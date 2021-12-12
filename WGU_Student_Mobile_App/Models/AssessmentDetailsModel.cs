using System;

namespace WGU_Student_Mobile_App.Models
{
    public class AssessmentDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CourseName { get; set; }

        public AssessmentTypes AssessmentType { get; set; }

        public AssessmentDetailsModel()
        {

        }
    }
}
