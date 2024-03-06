using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentsProblem.Models
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.Include(sc => sc.StudentCourses)
                .ThenInclude(c => c.Course)
                .OrderBy(s => s.StudentId)
                .ToListAsync();
        }

        public async Task<int> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            return await _context.SaveChangesAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.Include(sc => sc.StudentCourses)
                .ThenInclude(c => c.Course).FirstOrDefaultAsync(x=>x.StudentId == id);
        }

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
    }
}
