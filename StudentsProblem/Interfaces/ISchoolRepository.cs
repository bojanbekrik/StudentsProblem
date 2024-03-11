using StudentsProblem.Models;

namespace StudentsProblem.Interfaces
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAllSchoolsAsync();

        Task<School> GetSchoolByIdAsync(int id);

        Task<int> AddSchoolAsync(School school);

        Task<int> UpdateSchoolAsync(School school);

        Task<int> DeleteSchoolAsync(int id);
    }
}
