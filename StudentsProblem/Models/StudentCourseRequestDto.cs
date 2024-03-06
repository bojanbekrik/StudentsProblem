namespace StudentsProblem.Models
{
    public class StudentCourseRequestDto
    {
        public int Indeks { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public int[] CourseIds { get; set; }
    }
}
