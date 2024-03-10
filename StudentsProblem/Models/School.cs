namespace StudentsProblem.Models
{
    public class School
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public Address? Address { get; set; }

        //public List<Student>? Students { get; set; }
    }
}
// adresa mozes da kreiras bez da i postavis skolo na taa adresa
// skolo ne mozes da kreiras bez adresa