using System;

namespace WGU_Student_Mobile_App.Models
{
    public class NoteDetailsModel
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public NoteDetailsModel()
        {

        }

    }
}
