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
        public async Task<IActionResult> Add(StudentCourseViewModel scvm)
        {
            var student = new Student()
            {
                Indeks = scvm.Indeks,
                Name = scvm.Name,
                Surname = scvm.Surname
            };
            foreach (var course in scvm.CourseIds)
            {
                student.StudentCourses.Add(new StudentCourse()
                {
                    Student = student,
                    CoursesId = course
                });
            }
            await studentRepository.AddStudentAsync(student);
            return RedirectToAction("Index");
        }
    }
}
