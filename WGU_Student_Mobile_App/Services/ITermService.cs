using System;
using System.Collections.Generic;
using WGU_Student_Mobile_App.Models;

namespace WGU_Student_Mobile_App.Services
{
    public interface ITermService
    {
        void AddTerm(string name, DateTime startDate, DateTime endDate, bool hasNotified);
        void DeleteTerm(int Id);
        void EditTerm(int Id, string name, DateTime startDate, DateTime endDate, bool hasNotified);
        Term GetTerm(int id);
        TermDetailsModel GetTermDetails(int id);
        List<Term> GetTerms();
    }
}