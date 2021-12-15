namespace WGU_Student_Mobile_App.Services
{
    public interface IUserService
    {
        void AddUser(string username, string email, string password);
        int SignIn(string username, string password);
    }
}