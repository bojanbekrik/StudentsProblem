namespace StudentsProblem.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public School? School { get; set; }

        public Student? Student { get; set; }
    }
}
