using SQLite;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(InstructorService))]
namespace WGU_Student_Mobile_App.Services
{
    public class InstructorService : IInstructorService
    {
        private SQLiteConnection db;

        public InstructorService(SQLiteConnection database)
        {
            db = database;
        }

        public void AddInstructor(string name, string phoneNumber, string email)
        {
            Instructor instructor = new Instructor
            {
                UserId = DependencyService.Get<ILoggedInService>().Get(),
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email
            };

            int Id = db.Insert(instructor);
        }

        public void EditInstructor(int id, string name, string phoneNumber, string email)
        {
            SQLiteCommand cmd = db.CreateCommand(UpdateInstructor, name, phoneNumber, email, id);
            _ = cmd.ExecuteNonQuery();
        }

        public void DeleteInstructor(int Id)
        {
            db.Delete<Instructor>(Id);
        }

        public List<Instructor> GetInstructors()
        {
            int userId = DependencyService.Get<ILoggedInService>().Get();
            return db.Table<Instructor>().Where(i => i.UserId == userId).ToList();
        }

        public Instructor GetInstructor(int id)
        {
            var instructor = db.Table<Instructor>()
                .FirstOrDefault(i => i.Id == id);

            return instructor;
        }

        private const string UpdateInstructor = "UPDATE Instructor SET Name=?, PhoneNumber=?, Email=? WHERE Id=?;";

    }
}
