using SQLite;
using System;
using System.Collections.Generic;

namespace WGU_Student_Mobile_App.Models
{
    public class TermDetailsModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Course> Courses { get; set; }

        public TermDetailsModel()
        {

        }
    }
}
