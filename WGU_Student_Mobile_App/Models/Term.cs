using SQLite;
using System;

namespace WGU_Student_Mobile_App.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool HasNotified { get; set; }

        public Term()
        {

        }
    }
}
