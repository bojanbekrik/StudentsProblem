using StudentsProblem.Models;

namespace StudentsProblem.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();

        Task<int> AddStudentAsync(Student student);

        Task<Student?> GetStudentByIdAsync(int id);

        Task UpdateStudentAsync(Student student, IEnumerable<int> selectedCourseIds);

        Task<int> DeleteStudentAsync(int id);

        Task<IEnumerable<Student>> SearchStudentsAsync(string searchByNameOrSurname);

        Task<int> GetAllStudentsCountAsync();

        Task<IEnumerable<Student>> GetStudentsPagedAsync(int? pageNumber, int pageSize);
    }
}
