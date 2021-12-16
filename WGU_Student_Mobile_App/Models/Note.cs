using SQLite;
using System;

namespace WGU_Student_Mobile_App.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public Note()
        {

        }

    }
}
