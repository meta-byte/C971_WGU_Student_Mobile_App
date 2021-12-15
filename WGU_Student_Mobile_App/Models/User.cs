using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace WGU_Student_Mobile_App.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public User()
        {

        }
    }

}
