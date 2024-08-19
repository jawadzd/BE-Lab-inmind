using Microsoft.AspNetCore.Mvc;
using BE_Project.Repositories;
using BE_Project.Models;
using System.Globalization;
using BE_Project.Services;


namespace BE_Project.Controllers
{
    [ApiController]
    [Route("Students")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _studentRepository; //connecting to the repo
        private readonly StudentService _studentService;  //connecting to auth service 

        public StudentsController(StudentRepository studentRepository,StudentService studentService)
        {
            _studentRepository = studentRepository;
            _studentService = studentService;
        }


        //getting all students
        [HttpGet]
        public ActionResult GetStudents()
        {
            return Ok(_studentRepository.Students);
        }
        //getting student by id
        [HttpGet("{id}")]
        public ActionResult GetStudent([FromRoute] int id)
        {
            var student = _studentRepository.Students.Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found");//error handling
            }
            return Ok(student);
        }
        //adding a student
        [HttpPost]
        public ActionResult AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Invalid student data");
            }
            // Set the Id for the new student
            if (_studentRepository.Students.Any())
            {
                student.Id = _studentRepository.Students.Max(s => s.Id) + 1;
            }
            else
            {
                student.Id = 1; // If the list is empty, start with Id 1
            }
            _studentRepository.Students.Add(student);
            return NoContent();
        }
        //deleting a student by id 
        [HttpDelete("{id}")]
        public ActionResult DeleteStudent([FromRoute] int id)
        {
            var result = _studentService.DeleteStudent(id);
            if (!result)
            {
                return NotFound("Student not found");
            }
            return NoContent();
        }


        //searching a student by name it didnt work with httpget("{name}") so i used httpget("filter{name}") as it was contradicting with the id get as an api 
        [HttpGet("filter{name}")]
        public ActionResult SearchStudent([FromRoute] string name)
        {
            var students = _studentRepository.Students
                .Where(s => s.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                            s.LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(students);


        }

        //this i s a function that will return the currect date but will check for the query patameters and translate the language accordingly using the culture info system globalization 

        [HttpGet("current-date")]
        public ActionResult GetCurrentDate([FromHeader] string lang)
        {
            var culture = new CultureInfo("en-US"); // Default to en-US

            if (!string.IsNullOrEmpty(lang))
            {
                if (lang.Equals("es-ES", StringComparison.OrdinalIgnoreCase))
                {
                    culture = new CultureInfo("es-ES");
                }
                else if (lang.Equals("fr-FR", StringComparison.OrdinalIgnoreCase))
                {
                    culture = new CultureInfo("fr-FR");
                }
            }

            var currentDate = DateTime.Now.ToString("D", culture);
            return Ok(currentDate);
        }

        [HttpPut("{id}")]

        public ActionResult UpdateStudent([FromRoute] int id, [FromBody] UpdateStudentDto updatedStudent)
        {
           var result = _studentService.UpdateStudent(id, updatedStudent);
            if (!result)
            {
                return NotFound("Student not found");
            }
            return NoContent();

        }


        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
        {
            var filePath = await _studentService.UploadImage(image);
            if (filePath == null)
            { 
              return BadRequest("Invalid image file");
            }

            return Ok(filePath);
        }

    }
}