using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;
using System.Linq;

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

        public async Task<int> DeleteStudentAsync(int id)
        {
            var studentToDelete = await _context.Students.FirstOrDefaultAsync(s=>s.StudentId == id);

            if (studentToDelete != null)
            {
                _context.Students.Remove(studentToDelete);
                
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The student to delete can't be found.");
            }
        }

    }
}

