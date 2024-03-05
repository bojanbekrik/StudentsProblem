using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Models;

namespace StudentsProblem.Controllers
{
    [ApiController]
    [Route("courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return new JsonResult(courses);
        }
    }
}
