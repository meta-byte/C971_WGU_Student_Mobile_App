using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;

namespace WGU_Student_Mobile_App.Services
{
    public interface IInstructorService
    {
        void AddInstructor(string name, string phoneNumber, string email);
        void DeleteInstructor(int Id);
        void EditInstructor(int Id, string name, string phoneNumber, string email);
        Instructor GetInstructor(int id);
        List<Instructor> GetInstructors();
    }
}