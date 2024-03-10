namespace StudentsProblem.Models
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAllSchoolsAsync();

        Task<School> GetSchoolByIdAsync(int id);

        Task<int> AddSchoolAsync(School school);

        Task<int> UpdateSchoolAsync(School school);
    }
}
