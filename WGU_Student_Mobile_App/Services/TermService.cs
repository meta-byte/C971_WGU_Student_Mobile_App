using SQLite;
using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(TermService))]
namespace WGU_Student_Mobile_App.Services
{
    public class TermService : ITermService
    {
        private SQLiteConnection db;
        Term result;

        public TermService(SQLiteConnection database)
        {
            db = database;
        }

        public void AddTerm(string name, DateTime startDate, DateTime endDate, bool hasNotified)
        {
            Term term = new Term
            {
                UserId = DependencyService.Get<ILoggedInService>().Get(),
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                HasNotified = hasNotified,
            };

            int Id = db.Insert(term);
        }

        public void EditTerm(int id, string name, DateTime startDate, DateTime endDate, bool hasNotified)
        {

            SQLiteCommand cmd = db.CreateCommand(UpdateTerm, name, startDate, endDate, hasNotified, id);
            _ = cmd.ExecuteNonQuery();
        }

        public void DeleteTerm(int id)
        {
            db.Delete<Term>(id);
        }

        public List<Term> GetTerms()
        {
            int userId = DependencyService.Get<ILoggedInService>().Get();
            return db.Table<Term>().Where(a => a.UserId == userId).ToList();
        }

        public Term GetTerm(int id)
        {
            int userId = DependencyService.Get<ILoggedInService>().Get();
            var term = db.Table<Term>()
                .FirstOrDefault(t => t.Id == id && t.UserId == userId);

            return term;
        }

        public TermDetailsModel GetTermDetails(int id)
        {
            int userId = DependencyService.Get<ILoggedInService>().Get();
            SQLiteCommand cmd = db.CreateCommand(GetTermDetailsQuery, id, userId);
            foreach (TermDetailsModel td in cmd.ExecuteQuery<TermDetailsModel>())
            {
                SQLiteCommand crsCmd = db.CreateCommand(GetCoursesByTermId, id, userId);
                td.Courses = crsCmd.ExecuteQuery<Course>();

                return td;
            }
            return null;
        }

        private const string GetTermDetailsQuery = @"SELECT * FROM Term where Id=? AND UserId=?;";
        private const string GetCoursesByTermId = @"SELECT * FROM Course where TermId=? AND UserId=?;";

        private const string UpdateTerm = @"UPDATE Term SET Name=?, StartDate=?, EndDate=?, HasNotified=? WHERE Id=?;";

    }
}
