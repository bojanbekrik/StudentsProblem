using StudentsProblem.Models;

namespace StudentsProblem.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();

        Task<Course?> GetCourseByIdAsync(int id);

        Task<int> UpdateCourseAsync(Course course);

        Task<int> DeleteCourseAsync(int id);

        Task<int> AddCourseAsync(Course course);

        Task<int> GetAllCoursesCountAsync();

        Task<IEnumerable<Course>> GetCoursesPagedAsync(int? pageNumber, int pageSize);
    }
}
