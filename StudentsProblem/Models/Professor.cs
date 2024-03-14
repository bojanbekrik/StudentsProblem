namespace StudentsProblem.Models
{
    public class Professor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ProfessorCourse> ProfessorCourses { get; } = [];
    }
}
