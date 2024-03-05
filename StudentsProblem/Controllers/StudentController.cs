using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Models;

namespace StudentsProblem.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await studentRepository.GetAllStudentsAsync();
            return new JsonResult(students);
        }
    }
}
