using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;

namespace WGU_Student_Mobile_App.Services
{
    public interface IAssessmentService
    {
        void AddAssessment(int courseId, string name, DateTime startDate, DateTime endDate, AssessmentTypes assessmentType, bool hasNotified);
        void DeleteAssessment(int id);
        void EditAssessment(int id, int courseId, string name, DateTime startDate, DateTime endDate, AssessmentTypes assessmentType, bool hasNotified);
        Assessment GetAssessment(int id);
        AssessmentDetailsModel GetAssessmentDetails(int id);
        List<Assessment> GetAssessments();
    }
}