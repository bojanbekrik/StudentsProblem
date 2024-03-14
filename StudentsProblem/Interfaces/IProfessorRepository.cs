using StudentsProblem.Models;

namespace StudentsProblem.Interfaces
{
    public interface IProfessorRepository
    {
        public Task<IEnumerable<Professor>> GetAllProfessorsAsync();

        public Task<Professor> GetProfessorByIdAsync(int id);

        public Task<int> AddProfessorAsync(Professor professor);

        public Task<int> UpdateProfessorAsync(Professor professor, IEnumerable<int> selectedCourses);

        public Task<int> DeleteProfessorAsync(int id);
    }
}
