using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsProblem.Models;

namespace StudentsProblem.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;
        private readonly ApplicationDbContext context;

        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository, ApplicationDbContext context)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.context = context;     
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await studentRepository.GetAllStudentsAsync();
            return new JsonResult(students);
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentCourseRequestDto scrq)
        {
            var student = new Student()
            {
                Indeks = scrq.Indeks,
                Name = scrq.Name,
                Surname = scrq.Surname,
                SchoolId = scrq.SchoolId
            };

            foreach (var course in scrq.CourseIds)
            {
                student.StudentCourses.Add(new StudentCourse()
                {
                    Student = student,
                    CourseId = course
                });
            }

            await studentRepository.AddStudentAsync(student);
            return Ok(student);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Details(int id)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);
            return new JsonResult(student);
        }

        [HttpPut("{id}/update")] 
        public async Task<IActionResult> Update(int id, StudentCourseRequestDto scrd)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = scrd.Name;
            student.Surname = scrd.Surname;
            student.Indeks = scrd.Indeks;
            
            await studentRepository.UpdateStudentAsync(student, scrd.CourseIds);

            context.Students.Update(student);
            return Ok(student);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                await studentRepository.DeleteStudentAsync(id);
                return Ok(student);
            }
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Search(string searchByNameOrSurname)
        {
            if (searchByNameOrSurname != null)
            {
                var students = await studentRepository.SearchStudentsAsync(searchByNameOrSurname);
                return Ok(students);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
