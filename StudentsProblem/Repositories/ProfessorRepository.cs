using Microsoft.EntityFrameworkCore;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;

namespace StudentsProblem.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly ApplicationDbContext context;

        public ProfessorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Professor>> GetAllProfessorsAsync()
        {
            //return await context.Professors.OrderBy(p => p.Id).ToListAsync();
            return await context.Professors.Include(pc => pc.ProfessorCourses)
                .ThenInclude(c => c.Course)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<Professor> GetProfessorByIdAsync(int id)
        {
            return await context.Professors         
                .Include(pc => pc.ProfessorCourses)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> AddProfessorAsync(Professor professor)
        {
            context.Professors.Add(professor);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateProfessorAsync(Professor professor, IEnumerable<int> selectedCourseIds)
        {
            var existingIds = professor.ProfessorCourses.Select(pc => pc.CourseId).ToList();
            var toAdd = selectedCourseIds.Except(existingIds).ToList();
            var toRemove = existingIds.Except(selectedCourseIds).ToList();

            professor.ProfessorCourses.RemoveAll(pc => toRemove.Contains(pc.CourseId));

            foreach (var courseId in toAdd)
            {
                professor.ProfessorCourses.Add(new ProfessorCourse()
                {
                    CourseId = courseId
                });
            }
            return await context.SaveChangesAsync();

        }
        //NAPRAI MIGRACIJA ZA PROF COURSES. ..

        public async Task<int> DeleteProfessorAsync(int id)
        {
            var profToDelete = await context.Professors.FirstOrDefaultAsync(p => p.Id == id);
            if (profToDelete == null) 
            {
                throw new ArgumentException("The professor that you are trying to delete does not exist.");
            }
            context.Professors.Remove(profToDelete);
            return await context.SaveChangesAsync();
        }
    }
}
