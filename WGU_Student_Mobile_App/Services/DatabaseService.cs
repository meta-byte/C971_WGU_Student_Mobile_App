using SQLite;
using System;
using System.IO;
using WGU_Student_Mobile_App.Models;
using Xamarin.Essentials;

namespace WGU_Student_Mobile_App.Services
{
    class DatabaseService
    {


        SQLiteConnection db;
        public DatabaseService()
        {

        }

        public SQLiteConnection InitializeDatabase()
        {
            try
            {
                string databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
                bool createDbFile = !File.Exists(databasePath);
                if (createDbFile)
                {
                    File.Create(databasePath).Dispose();
                }
                db = new SQLiteConnection(databasePath);

                if (createDbFile)
                {
                    DropAllTables();
                    CreateAllTables();
                    PopulateAllTables();
                }

                return db;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to initialize the database" + ex);
                return db;
            }
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

        public void PopulateAllTables()
        {
            PopulateTerms();
            PopulateCourses();
            PopulateAssessments();
            PopulateInstructors();
            PopulateNotes();
            PopulateUsers();
        }

        public void PopulateTerms()
        {
            db.Insert(new Term
            {
                UserId = 1,
                Name = "Term1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                HasNotified = false,
            });

            db.Insert(new Term
            {
                UserId = 1,
                Name = "Term2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                HasNotified = false,
            });
        }

        public void PopulateAssessments()
        {
            db.Insert(new Assessment
            {
                UserId = 1,
                Name = "Ass1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                AssessmentType = AssessmentTypes.Objective,
                CourseId = 1,
                HasNotified = false,
            });

            db.Insert(new Assessment
            {
                UserId = 1,
                Name = "Ass2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                AssessmentType = AssessmentTypes.Performance,
                CourseId = 1,
                HasNotified = false,
            });

            db.Insert(new Assessment
            {
                UserId = 1,
                Name = "Ass3",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                AssessmentType = AssessmentTypes.Objective,
                CourseId = 2,
                HasNotified = false,
            });

            db.Insert(new Assessment
            {
                UserId = 1,
                Name = "Ass4",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                AssessmentType = AssessmentTypes.Performance,
                CourseId = 2,
                HasNotified = false,
            });
        }

        public void PopulateNotes()
        {
            db.Insert(new Note
            {
                UserId = 1,
                Name = "Note1",
                Description = "Great",
                CreatedDate = DateTime.Now,
                CourseId = 1,
            }); ;

            db.Insert(new Note
            {
                UserId = 1,
                Name = "Note2",
                Description = "Great",
                CreatedDate = DateTime.Now,
                CourseId = 1,
            });

            db.Insert(new Note
            {
                UserId = 1,
                Name = "Note3",
                Description = "Great",
                CreatedDate = DateTime.Now,
                CourseId = 2,
            });

            db.Insert(new Note
            {
                UserId = 1,
                Name = "Note4",
                Description = "Great",
                CreatedDate = DateTime.Now,
                CourseId = 2,
            });
        }

        public void PopulateInstructors()
        {
            db.Insert(new Instructor
            {
                UserId = 1,
                Name = "Garrett Howard",
                PhoneNumber = "801-592-0371",
                Email = "hylander.garrett@gmail.com"
            });

            db.Insert(new Instructor
            {
                UserId = 1,
                Name = "Instructor2",
                PhoneNumber = "801-999-9999",
                Email = "whyme2@why.com"
            });

        }

        public void PopulateCourses()
        {
            db.Insert(new Course
            {
                UserId = 1,
                Name = "Course1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                TermId = 1,
                InstructorId = 1,
                CourseStatus = CourseStatuses.In_Progress,
                HasNotified = false
            });

            db.Insert(new Course
            {
                UserId = 1,
                Name = "Course2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                TermId = 2,
                InstructorId = 2,
                CourseStatus = CourseStatuses.Enrolled,
                HasNotified = false
            });
        }

        public void PopulateUsers()
        {
            db.Insert(new User
            {
                Username = "test",
                Email = "test@test.com",
                Password = "test",
            });

            db.Insert(new User
            {
                Username = "test2",
                Email = "test2@test.com",
                Password = "test2",
            });
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