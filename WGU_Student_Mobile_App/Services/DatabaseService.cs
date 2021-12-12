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

        public void DropAllTables()
        {
            db.Execute(DropTermTables);
            db.Execute(DropInstructorTables);
            db.Execute(DropCourseTables);
            db.Execute(DropAssessmentTables);
            db.Execute(DropNoteTables);
        }

        public void CreateAllTables()
        {
            db.Execute(CreateTermTable);
            db.Execute(CreateInstructorTable);
            db.Execute(CreateCourseTable);
            db.Execute(CreateAssessmentTable);
            db.Execute(CreateNoteTable);
        }

        public void PopulateAllTables()
        {
            PopulateTerms();
            PopulateCourses();
            PopulateAssessments();
            PopulateInstructors();
            PopulateNotes();
        }

        public void PopulateTerms()
        {
            db.Insert(new Term
            {
                Name = "Term1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                HasNotified = false,
            });

            db.Insert(new Term
            {
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
                Name = "Ass1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                AssessmentType = AssessmentTypes.Objective,
                CourseId = 1,
                HasNotified = false,
            });

            db.Insert(new Assessment
            {
                Name = "Ass2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                AssessmentType = AssessmentTypes.Performance,
                CourseId = 1,
                HasNotified = false,
            });

            db.Insert(new Assessment
            {
                Name = "Ass3",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                AssessmentType = AssessmentTypes.Objective,
                CourseId = 2,
                HasNotified = false,
            });

            db.Insert(new Assessment
            {
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
                Name = "Note1",
                Description = "Great",
                CreatedDate = DateTime.Now,
                CourseId = 1,
            }); ;

            db.Insert(new Note
            {
                Name = "Note2",
                Description = "Great",
                CreatedDate = DateTime.Now,
                CourseId = 1,
            });

            db.Insert(new Note
            {
                Name = "Note3",
                Description = "Great",
                CreatedDate = DateTime.Now,
                CourseId = 2,
            });

            db.Insert(new Note
            {
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
                Name = "Garrett Howard",
                PhoneNumber = "801-592-0371",
                Email = "hylander.garrett@gmail.com"
            });

            db.Insert(new Instructor
            {
                Name = "Instructor2",
                PhoneNumber = "801-999-9999",
                Email = "whyme2@why.com"
            });

        }

        public void PopulateCourses()
        {
            db.Insert(new Course
            {
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
                Name = "Course2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                TermId = 2,
                InstructorId = 2,
                CourseStatus = CourseStatuses.Enrolled,
                HasNotified = false
            });
        }

        private const string DropTermTables = @"DROP TABLE IF EXISTS Term;";
        private const string DropInstructorTables = @"DROP TABLE IF EXISTS Instructor;";
        private const string DropCourseTables = @"DROP TABLE IF EXISTS Course;";
        private const string DropAssessmentTables = @"DROP TABLE IF EXISTS Assessment;";
        private const string DropNoteTables = @"DROP TABLE IF EXISTS Note;";

        private const string CreateTermTable = @"CREATE TABLE IF NOT EXISTS Term (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, StartDate DATETIME, EndDate DATETIME, HasNotified INTEGER)";
        private const string CreateInstructorTable = @"CREATE TABLE IF NOT EXISTS Instructor (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, PhoneNumber TEXT, Email TEXT)";
        private const string CreateCourseTable = @"CREATE TABLE IF NOT EXISTS Course (Id INTEGER PRIMARY KEY AUTOINCREMENT, TermId INTEGER, InstructorId INTEGER, Name TEXT, StartDate DATETIME, EndDate DATETIME, CourseStatus INTEGER, HasNotified INTEGER)";
        private const string CreateAssessmentTable = @"CREATE TABLE IF NOT EXISTS Assessment (Id INTEGER PRIMARY KEY AUTOINCREMENT, CourseId INTEGER, Name TEXT, StartDate DATETIME, EndDate DATETIME, AssessmentType INTEGER, HasNotified INTEGER)";
        private const string CreateNoteTable = @"CREATE TABLE IF NOT EXISTS Note (Id INTEGER PRIMARY KEY AUTOINCREMENT, CourseId INTEGER, Name TEXT, Description TEXT, CreatedDate DATETIME)";
    }
}
