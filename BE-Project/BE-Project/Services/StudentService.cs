using BE_Project.Models;
using BE_Project.Repositories;

namespace BE_Project.Services
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public bool UpdateStudent(int id, UpdateStudentDto updatedStudent)
        {
            var student = _studentRepository.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(updatedStudent.FirstName))
            {
                student.FirstName = updatedStudent.FirstName;
            }

            if (!string.IsNullOrEmpty(updatedStudent.LastName))
            {
                student.LastName = updatedStudent.LastName;
            }

            if (!string.IsNullOrEmpty(updatedStudent.Email))
            {
                student.Email = updatedStudent.Email;
            }

            if (!string.IsNullOrEmpty(updatedStudent.FatherName))
            {
                student.FatherName = updatedStudent.FatherName;
            }

            if (!string.IsNullOrEmpty(updatedStudent.MotherName))
            {
                student.MotherName = updatedStudent.MotherName;
            }

            if (updatedStudent.Age != 0)
            {
                student.Age = updatedStudent.Age;
            }

            return true;
        }
        public bool AddStudent(Student student)
        {
            if (student == null)
            {
                return false;
            }
            _studentRepository.Students.Add(student);
            return true;
        }

        public bool DeleteStudent(int id)
        {
            var student = _studentRepository.Students.Find(s => s.Id == id);
            if (student == null)
            {
                return false;
            }
            _studentRepository.Students.Remove(student);
            return true;
        }

        public async Task<string> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return null;
            }

            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var filePath = Path.Combine(uploadsFolderPath, image.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"/uploads/{image.FileName}";
        }
    }
}