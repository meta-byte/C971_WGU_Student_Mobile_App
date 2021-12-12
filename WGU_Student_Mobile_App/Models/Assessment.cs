using SQLite;
using System;

namespace WGU_Student_Mobile_App.Models
{
    [Flags]
    public enum AssessmentTypes
    {
        NA,
        Performance,
        Objective,
    }
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CourseId { get; set; } //Associated Course

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public AssessmentTypes AssessmentType { get; set; }

        public bool HasNotified { get; set; }

        public Assessment()
        {

        }
    }
}
