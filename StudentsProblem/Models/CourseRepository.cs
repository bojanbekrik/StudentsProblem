

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

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x=>x.CourseId == id);
        }

        public async Task<int> UpdateCourseAsync(Course course)
        {
            var c = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == course.CourseId);

            if (c != null)
            {
                c.CourseName = course.CourseName;
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Course to update cant be found");
            }
        }

        public async Task<int> DeleteCourseAsync(int id)
        {
            var c = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);

            if (c == null)
            {
                throw new ArgumentException("Course to delete cant be found");
            }

            _context.Courses.Remove(c);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            return await _context.SaveChangesAsync();
        }
    }
}
