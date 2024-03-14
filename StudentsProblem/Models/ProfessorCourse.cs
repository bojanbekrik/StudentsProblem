namespace StudentsProblem.Models
{
    public class ProfessorCourse
    {
        public int Id { get; set; }

        public int ProfessorId { get; set; }

        public int CourseId { get; set; }

        public Professor? Professor { get; set; } = null;

        public Course? Course { get; set; } = null;
    }
}
