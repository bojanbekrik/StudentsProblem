using Microsoft.EntityFrameworkCore;

namespace StudentsProblem.Models
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ApplicationDbContext context;

        public SchoolRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await context.School
                .Include(a => a.Address)
                .OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<School> GetSchoolByIdAsync(int id)
        {
            var school = await context.School
                .Include(a => a.Address)
                .FirstOrDefaultAsync(sch => sch.Id == id);
                
            if (school == null)
            {
                throw new ArgumentException("School with that id can not be found");
            }
            else
            {
                return school;
            }
        }

        public async Task<int> AddSchoolAsync(School school)
        {
            context.School.Add(school);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateSchoolAsync(School school)
        {
            context.School.Update(school);
            return await context.SaveChangesAsync();
        }

        /*
        public async Task UpdateStudentAsync(Student student, IEnumerable<int> selectedCourseIds)
        {
            var existingIds = student.StudentCourses.Select(sc => sc.CourseId).ToList();
            var toAdd = selectedCourseIds.Except(existingIds).ToList();
            var toRemove = existingIds.Except(selectedCourseIds).ToList();

            student.StudentCourses.RemoveAll(sc => toRemove.Contains(sc.CourseId));

            foreach (var courseId in toAdd)
            {
                student.StudentCourses.Add(new StudentCourse()
                {
                    CourseId = courseId
                });
            }

            await _context.SaveChangesAsync();
        } 
        */
    }
}
