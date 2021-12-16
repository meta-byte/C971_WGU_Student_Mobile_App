using SQLite;
using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;
using WGU_Student_Mobile_App.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoggedInService))]
namespace WGU_Student_Mobile_App.Services
{
    public class LoggedInService : ILoggedInService
    {
        int id;

        public LoggedInService()
        {
        }

        public int Get()
        {
            return id;
        }

        public void Set(int userId)
        {
            id = userId;
        }
    }
}
