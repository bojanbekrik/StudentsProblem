using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Models;

namespace StudentsProblem.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await studentRepository.GetAllStudentsAsync();
            return new JsonResult(students);
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentCourse studentCourse)
        {
            try
            {
                Course course = await courseRepository.GetCourseByIdAsync(studentCourse.CoursesId);

                Student student = new()
                {
                    Indeks = studentCourse.Student.Indeks,
                    Name = studentCourse.Student.Name,
                    Surname = studentCourse.Student.Surname,
                    StudentCourses = new List<StudentCourse> { new StudentCourse { Course = course } }
                };
                await studentRepository.AddStudentAsync(student);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw new ArgumentException("some adding error...");
            }
        }
    }
}
