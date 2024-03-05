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
            return await _context.Students.OrderBy(x=>x.StudentId).ToListAsync();
        }
    }
}
