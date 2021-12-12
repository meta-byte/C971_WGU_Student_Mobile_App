using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;

namespace WGU_Student_Mobile_App.Services
{
    public interface INoteService
    {
        void AddNote(int courseId, string name, string description, DateTime createdDate);
        void DeleteNote(int Id);
        void EditNote(int id, int courseId, string name, string description, DateTime createdDate);
        Note GetNote(int id);
        NoteDetailsModel GetNoteDetails(int id);
        List<Note> GetNotes();
    }
}