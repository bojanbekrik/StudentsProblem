using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;
using StudentsProblem.Utilities;
using System.Security.Cryptography.Xml;

namespace StudentsProblem.Controllers
{
    [ApiController]
    [Route("courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;

        private int pageSize = 2;

        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            var courses = await courseRepository.GetAllCoursesAsync();
            return new JsonResult(courses);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Details(int id)
        {
            var course = await courseRepository.GetCourseByIdAsync(id);
            return new JsonResult(course);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(Course course)
        {
            var c = await courseRepository.GetCourseByIdAsync(course.CourseId);

            if (c==null)
            {
                return NotFound();
            }

            c.CourseName = course.CourseName;
            await courseRepository.UpdateCourseAsync(c);
            return Ok(c);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await courseRepository.GetCourseByIdAsync(id);

            if (c == null)
            {
                return NotFound();
            }

            await courseRepository.DeleteCourseAsync(id);
            return Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Course course)
        {
            await courseRepository.AddCourseAsync(course);
            return Ok(course);
        }

        [HttpGet("/paginationOfCourses")]
        public async Task<IActionResult> IndexPaging(int? pageNumber)
        {
            var courses = await courseRepository.GetCoursesPagedAsync(pageNumber, pageSize);

            pageNumber ??= 1;

            var count = await courseRepository.GetAllCoursesCountAsync();

            return new JsonResult(new PaginatedList<Course>(courses.ToList(), count, pageNumber.Value, pageSize));
        }

    }
}
