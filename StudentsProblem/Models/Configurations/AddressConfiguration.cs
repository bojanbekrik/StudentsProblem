using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace StudentsProblem.Models.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(10);

            //adresa so skolo one to one
            builder
                .HasOne(sch => sch.School)
                .WithOne(a => a.Address)
                .OnDelete(DeleteBehavior.ClientCascade);

            //adresa so student one to one
            builder
                .HasOne(stu => stu.Student)
                .WithOne(a => a.Address)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
