using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SQLite;
using Microsoft.Data.Sqlite;
using WGU_Student_Mobile_App.Models;

namespace WGU_Student_Mobile_App.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        SQLiteConnection db;

        public SQLiteConnection InitializeDatabase()
        {
            db = new SQLiteConnection(":memory:");
            DropAllTables();
            CreateAllTables();

            return db;
        }

        [TestMethod]
        public void TestAddUser_Success()
        {
            InitializeDatabase();

            User user = new User
            {
                Username = "test",
                Email = "test@test.com",
                Password = "test",
            };

            int Id = db.Insert(user);

            Assert.AreEqual(1, Id);
            db.Close();
        }

        [TestMethod]
        public void TestAddTerm_Success()
        {
            InitializeDatabase();

            Term term = new Term
            {
                UserId = 1,
                Name = "Term1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                HasNotified = false,
            };

            int Id = db.Insert(term);

            Assert.AreEqual(1, Id);
            db.Close();
        }

        [TestMethod]
        public void TestEditTerm_Success()
        {
            InitializeDatabase();
            Term term = new Term
            {
                UserId = 1,
                Name = "Term1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                HasNotified = false,
            };
            int Id = db.Insert(term);

            Term termEdit = new Term
            {
                UserId = 1,
                Name = "Edit1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                HasNotified = false,
            };

            const string UpdateTerm = @"UPDATE Term SET Name=?, StartDate=?, EndDate=?, HasNotified=? WHERE Id=?;";
            SQLiteCommand cmd = db.CreateCommand(UpdateTerm, termEdit.Name, termEdit.Name, termEdit.EndDate, termEdit.HasNotified, termEdit.UserId);
            _ = cmd.ExecuteNonQuery();

            var updatedTerm = db.Table<Term>()
                .FirstOrDefault(t => t.Id == 1 && t.UserId == 1);

            Assert.AreEqual(updatedTerm.ToString(), termEdit.ToString());
            db.Close();
        }

        public void DropAllTables()
        {
            db.Execute(DropTermTables);
            db.Execute(DropInstructorTables);
            db.Execute(DropCourseTables);
            db.Execute(DropAssessmentTables);
            db.Execute(DropNoteTables);
            db.Execute(DropUserTables);
        }

        public void CreateAllTables()
        {
            db.Execute(CreateTermTable);
            db.Execute(CreateInstructorTable);
            db.Execute(CreateCourseTable);
            db.Execute(CreateAssessmentTable);
            db.Execute(CreateNoteTable);
            db.Execute(CreateUserTable);
        }

        private const string DropTermTables = @"DROP TABLE IF EXISTS Term;";
        private const string DropInstructorTables = @"DROP TABLE IF EXISTS Instructor;";
        private const string DropCourseTables = @"DROP TABLE IF EXISTS Course;";
        private const string DropAssessmentTables = @"DROP TABLE IF EXISTS Assessment;";
        private const string DropNoteTables = @"DROP TABLE IF EXISTS Note;";
        private const string DropUserTables = @"DROP TABLE IF EXISTS User;";


        private const string CreateTermTable = @"CREATE TABLE IF NOT EXISTS Term (Id INTEGER PRIMARY KEY AUTOINCREMENT, UserId INTEGER, Name TEXT, StartDate DATETIME, EndDate DATETIME, HasNotified INTEGER)";
        private const string CreateInstructorTable = @"CREATE TABLE IF NOT EXISTS Instructor (Id INTEGER PRIMARY KEY AUTOINCREMENT, UserId INTEGER, Name TEXT, PhoneNumber TEXT, Email TEXT)";
        private const string CreateCourseTable = @"CREATE TABLE IF NOT EXISTS Course (Id INTEGER PRIMARY KEY AUTOINCREMENT, UserId INTEGER, TermId INTEGER, InstructorId INTEGER, Name TEXT, StartDate DATETIME, EndDate DATETIME, CourseStatus INTEGER, HasNotified INTEGER)";
        private const string CreateAssessmentTable = @"CREATE TABLE IF NOT EXISTS Assessment (Id INTEGER PRIMARY KEY AUTOINCREMENT, UserId INTEGER, CourseId INTEGER, Name TEXT, StartDate DATETIME, EndDate DATETIME, AssessmentType INTEGER, HasNotified INTEGER)";
        private const string CreateNoteTable = @"CREATE TABLE IF NOT EXISTS Note (Id INTEGER PRIMARY KEY AUTOINCREMENT, UserId INTEGER, CourseId INTEGER, Name TEXT, Description TEXT, CreatedDate DATETIME)";
        private const string CreateUserTable = @"CREATE TABLE IF NOT EXISTS User (Id INTEGER PRIMARY KEY AUTOINCREMENT, UserId INTEGER, Username TEXT UNIQUE, Email TEXT, Password TEXT)";

    }
}
