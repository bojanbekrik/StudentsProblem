namespace StudentsProblem.Models
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
    }
}
