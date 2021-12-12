using SQLite;
using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CourseService))]
namespace WGU_Student_Mobile_App.Services
{
    public class CourseService : ICourseService
    {
        private SQLiteConnection db;

        public CourseService(SQLiteConnection database)
        {
            db = database;
        }

        public void AddCourse(int termId, string name, DateTime startDate, DateTime endDate, int instructorId, CourseStatuses status, bool hasNotified)
        {
            Course course = new Course
            {
                TermId = termId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                InstructorId = instructorId,
                CourseStatus = status,
                HasNotified = hasNotified,
            };

            int Id = db.Insert(course);
        }

        public void EditCourse(int termId, int id, string name, DateTime startDate, DateTime endDate, int instructorId, CourseStatuses status, bool hasNotified)
        {

            SQLiteCommand cmd = db.CreateCommand(UpdateCourse, name, termId, instructorId, startDate, endDate, status, hasNotified, id);
            _ = cmd.ExecuteNonQuery();
        }

        public void DeleteCourse(int Id)
        {
            db.Delete<Course>(Id);
        }

        public List<Course> GetCourses()
        {
            return db.Table<Course>().ToList();
        }

        public Course GetCourse(int id)
        {
            var course = db.Table<Course>()
                .FirstOrDefault(c => c.Id == id);

            return course;
        }

        public CourseDetailsModel GetCourseDetails(int id)
        {
            SQLiteCommand cmd = db.CreateCommand(GetCourseDetailsQuery, id);
            foreach (CourseDetailsModel cd in cmd.ExecuteQuery<CourseDetailsModel>())
            {
                SQLiteCommand assCmd = db.CreateCommand(GetAssessmentsByCourseID, id);
                cd.Assessments = assCmd.ExecuteQuery<Assessment>();

                SQLiteCommand notesCmd = db.CreateCommand(GetNotesByCourseID, id);
                cd.Notes = notesCmd.ExecuteQuery<Note>();

                return cd;
            }
            return null;
        }

        private const string GetAssessmentsByCourseID = @"SELECT * FROM Assessment where CourseId=?;";
        private const string GetNotesByCourseID = @"SELECT * FROM Note where CourseId=?;";
        private const string UpdateCourse = "UPDATE Course SET Name=?, TermId=?, InstructorId=?, StartDate=?, EndDate=?, CourseStatus=?, HasNotified=? WHERE Id=?;";
        private const string GetCourseDetailsQuery = @"
    SELECT 
        Course.Id as Id, 
        Course.Name as Name, 
        Course.StartDate as StartDate, 
        Course.EndDate as EndDate, 
        Term.Name as TermName, 
        Instructor.Name as InstructorName, 
        Course.CourseStatus as CourseStatus 
    FROM 
    Course 
    JOIN Term 
        ON Course.TermId=Term.Id
    JOIN Instructor
        ON Course.InstructorId=Instructor.Id
    WHERE Course.Id=?;";
    }
}
