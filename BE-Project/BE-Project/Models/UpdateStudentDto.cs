namespace BE_Project.Models
{

    //this is a class that will be used to update a student
    public class UpdateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }


    }
}