

using Microsoft.EntityFrameworkCore;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;

namespace StudentsProblem.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext context;

        public CourseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            //return await _context.Courses.OrderBy(x => x.CourseId).ToListAsync(); 
            return await context.Courses
                .Include(sc => sc.StudentCourses)
                .ThenInclude(s => s.Student)
                .OrderBy(x => x.CourseId).ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
        }

        public async Task<int> UpdateCourseAsync(Course course)
        {
            var c = await context.Courses.FirstOrDefaultAsync(x => x.CourseId == course.CourseId);

            if (c != null)
            {
                c.CourseName = course.CourseName;
                return await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Course to update cant be found");
            }
        }

        public async Task<int> DeleteCourseAsync(int id)
        {
            var c = await context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);

            if (c == null)
            {
                throw new ArgumentException("Course to delete cant be found");
            }

            context.Courses.Remove(c);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddCourseAsync(Course course)
        {
            context.Courses.Add(course);
            return await context.SaveChangesAsync();
        }

        public async Task<int> GetAllCoursesCountAsync()
        {
            IQueryable<Course> allCourses = from c in context.Courses select c;
            var count = await allCourses.CountAsync();

            return count;
        }

        public async Task<IEnumerable<Course>> GetCoursesPagedAsync(int? pageNumber, int pageSize)
        {
            IQueryable<Course> courses = from c in context.Courses select c;

            pageNumber ??= 1;

            courses = courses.Skip((pageNumber.Value-1) * pageSize).Take(pageSize);

            return await courses.AsNoTracking().ToListAsync();
        }
    }
}
