using SQLite;
using System;

namespace WGU_Student_Mobile_App.Models
{
    [Flags]
    public enum CourseStatuses
    {
        NA,
        Enrolled,
        In_Progress,
        Dropped,
        Completed,
    }

    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TermId { get; set; }

        public int InstructorId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CourseStatuses CourseStatus { get; set; }

        public bool HasNotified { get; set; }

        public Course()
        {

        }

    }
}
