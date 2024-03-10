namespace StudentsProblem.Models
{
    public class School
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Address? Address { get; set; }

        public List<Student>? Students { get; set; }
    }
}
