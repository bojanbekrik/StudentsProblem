namespace StudentsProblem.Dtos
{
    public class StudentCourseRequestDto
    {
        public int Indeks { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int SchoolId { get; set; }

        public int[] CourseIds { get; set; }

        public int AddressId { get; set; }
    }
}
