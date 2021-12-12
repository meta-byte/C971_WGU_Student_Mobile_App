using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;

namespace WGU_Student_Mobile_App.Services
{
    public interface ICourseService
    {
        void AddCourse(int termId, string name, DateTime startDate, DateTime endDate, int instructorId, CourseStatuses status, bool hasNotified);
        void DeleteCourse(int Id);
        void EditCourse(int termId, int Id, string name, DateTime startDate, DateTime endDate, int instructorId, CourseStatuses status, bool hasNotified);
        Course GetCourse(int id);
        CourseDetailsModel GetCourseDetails(int id);
        List<Course> GetCourses();
    }
}