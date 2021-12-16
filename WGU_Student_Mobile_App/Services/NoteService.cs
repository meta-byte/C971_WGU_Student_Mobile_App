using SQLite;
using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoteService))]
namespace WGU_Student_Mobile_App.Services
{
    public class NoteService : INoteService
    {
        private SQLiteConnection db;

        public NoteService(SQLiteConnection database)
        {
            db = database;
        }

        public void AddNote(int courseId, string name, string description, DateTime createdDate)
        {
            Note note = new Note
            {
                UserId = DependencyService.Get<ILoggedInService>().Get(),
                CourseId = courseId,
                Name = name,
                Description = description,
                CreatedDate = createdDate,
            };

            int Id = db.Insert(note);
        }

        public void EditNote(int id, int courseId, string name, string description, DateTime createdDate)
        {
            SQLiteCommand cmd = db.CreateCommand(UpdateNote, name, courseId, description, createdDate, id);
            _ = cmd.ExecuteNonQuery();
        }

        public void DeleteNote(int Id)
        {
            db.Delete<Note>(Id);
        }

        public List<Note> GetNotes()
        {
            int userId = DependencyService.Get<ILoggedInService>().Get();
            return db.Table<Note>().Where(a => a.UserId == userId).ToList();
        }

        public Note GetNote(int id)
        {
            int userId = DependencyService.Get<ILoggedInService>().Get();
            var note = db.Table<Note>()
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            return note;
        }

        public NoteDetailsModel GetNoteDetails(int id)
        {
            int userId = DependencyService.Get<ILoggedInService>().Get();
            SQLiteCommand cmd = db.CreateCommand(GetNoteDetailsQuery, id, userId);
            foreach (NoteDetailsModel nd in cmd.ExecuteQuery<NoteDetailsModel>())
            {
                return nd;

            }
            return null;
        }

        private const string UpdateNote = "UPDATE Note SET Name=?, CourseId=?, Description=?, CreatedDate=? WHERE Id=?;";
        private const string GetNoteDetailsQuery = @"
    SELECT 
        Note.Id as Id, 
        Note.Name as Name, 
        Note.Description as Description, 
        Note.CreatedDate as CreatedDate, 
        Course.Name as CourseName 
    FROM 
    Note 
    JOIN Course 
        ON Note.CourseId=Course.Id
    WHERE Note.Id=? AND UserId=?;";
    }

}

