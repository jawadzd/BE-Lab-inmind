using System.Collections.Generic;
using BE_Project.Models;

namespace BE_Project.Repositories
{
    public class StudentRepository
    {

        private static readonly StudentRepository _instance = new StudentRepository();
        public List<Student> Students { get; private set; }

        // Private constructor to prevent instantiation
        private StudentRepository()
        {
            Students = new List<Student>
            {
                new Student { Id = 1, FirstName = "John", LastName = "Doe", FatherName = "Michael Doe", MotherName = "Jane Doe", Email = "john.doe@example.com", Age = 20 },
                new Student { Id = 2, FirstName = "Jane", LastName = "Smith", FatherName = "Robert Smith", MotherName = "Emily Smith", Email = "jane.smith@example.com", Age = 22 },
                new Student { Id = 3, FirstName = "Alice", LastName = "Johnson", FatherName = "David Johnson", MotherName = "Sarah Johnson", Email = "alice.johnson@example.com", Age = 21 }
            };
        }

        // Public property to get the singleton instance
        public static StudentRepository Instance
        {
            get { return _instance; }


        }
    }
}