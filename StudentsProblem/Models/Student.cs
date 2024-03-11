namespace StudentsProblem.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        
        public int Indeks { get; set; }
        
        public string Name { get; set; }

        public string Surname { get; set; }

        public List<StudentCourse> StudentCourses { get; } = [];

        public int SchoolId { get; set; }

        public School School { get; set; } 
    }
}
