

using Microsoft.EntityFrameworkCore;

namespace StudentsProblem.Models
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.OrderBy(x => x.CourseId).ToListAsync(); 
        }
    }
}
