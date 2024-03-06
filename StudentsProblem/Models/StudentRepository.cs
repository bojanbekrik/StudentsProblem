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
    }
}
