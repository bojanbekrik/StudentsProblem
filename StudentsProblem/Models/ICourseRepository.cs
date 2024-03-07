namespace StudentsProblem.Models
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();

        Task<Course?> GetCourseByIdAsync(int id);

        Task<int> UpdateCourseAsync(Course course);

        Task<int> DeleteCourseAsync(int id);
    }
}
