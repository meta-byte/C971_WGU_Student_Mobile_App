using SQLite;
using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AssessmentService))]
namespace WGU_Student_Mobile_App.Services
{
    public class AssessmentService : IAssessmentService
    {
        private SQLiteConnection db;

        public AssessmentService(SQLiteConnection database)
        {
            db = database;
        }

        public void AddAssessment(int courseId, string name, DateTime startDate, DateTime endDate, AssessmentTypes assessmentType, bool hasNotified)
        {
            Assessment assessment = new Assessment
            {
                CourseId = courseId,
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                AssessmentType = assessmentType,
                HasNotified = hasNotified,
            };

            int Id = db.Insert(assessment);
        }

        public void EditAssessment(int id, int courseId, string name, DateTime startDate, DateTime endDate, AssessmentTypes assessmentType, bool hasNotified)
        {
            SQLiteCommand cmd = db.CreateCommand(UpdateAssessment, name, courseId, startDate, endDate, assessmentType, hasNotified, id);
            _ = cmd.ExecuteNonQuery();
        }

        public void DeleteAssessment(int id)
        {
            db.Delete<Assessment>(id);
        }

        public List<Assessment> GetAssessments()
        {
            return db.Table<Assessment>().ToList();
        }

        public Assessment GetAssessment(int id)
        {
            var assessment = db.Table<Assessment>()
                .FirstOrDefault(a => a.Id == id);

            return assessment;
        }

        public AssessmentDetailsModel GetAssessmentDetails(int id)
        {
            SQLiteCommand cmd = db.CreateCommand(GetAssessmentDetailsQuery, id);
            foreach (AssessmentDetailsModel ad in cmd.ExecuteQuery<AssessmentDetailsModel>())
            {
                return ad;

            }
            return null;
        }

        private const string UpdateAssessment = "UPDATE Assessment SET Name=?, CourseId=?, StartDate=?, EndDate=?, AssessmentType=?, HasNotified=? WHERE Id=?;";
        private const string GetAssessmentDetailsQuery = @"
    SELECT 
        Assessment.Id as Id, 
        Assessment.Name as Name, 
        Assessment.StartDate as StartDate, 
        Assessment.EndDate as EndDate,
        Assessment.AssessmentType,
        Course.Name as CourseName 
    FROM 
    Assessment 
    JOIN Course 
        ON Assessment.CourseId=Course.Id
    WHERE Assessment.Id=?;";
    }
}

