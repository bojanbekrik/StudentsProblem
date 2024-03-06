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
        public async Task<IActionResult> Add(StudentCourseRequestDto scrq)
        {
            var student = new Student()
            {
                Indeks = scrq.Indeks,
                Name = scrq.Name,
                Surname = scrq.Surname
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
            return RedirectToAction("Index");
        }
    }
}
