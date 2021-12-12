using SQLite;


namespace WGU_Student_Mobile_App.Models
{
    public class Instructor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Instructor()
        {

        }
    }

}
