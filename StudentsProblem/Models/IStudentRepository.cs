namespace StudentsProblem.Models
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();

        Task<int> AddStudentAsync(Student student);
    }
}
