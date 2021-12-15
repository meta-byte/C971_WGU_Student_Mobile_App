using SQLite;
using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]
namespace WGU_Student_Mobile_App.Services
{
    public class UserService : IUserService
    {
        private SQLiteConnection db;

        public UserService(SQLiteConnection database)
        {
            db = database;
        }

        public void AddUser(string username, string email, string password)
        {
            User user = new User
            {
                Username = username,
                Email = email,
                Password = password,
            };

            int Id = db.Insert(user);
        }

        public int SignIn(string username, string password)
        {
            SQLiteCommand cmd = db.CreateCommand(GetLoggedInQuery, username, password);
            foreach (int id in cmd.ExecuteQuery<int>())
            {
                return id;
            }
            return 0;
        }

        private const string GetLoggedInQuery = "SELECT Id FROM User WHERE Username=? AND Password=?;";

    }
}
