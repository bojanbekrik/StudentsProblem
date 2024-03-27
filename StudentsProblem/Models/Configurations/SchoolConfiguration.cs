using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace StudentsProblem.Models.Configurations
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.Property(s => s.Name).HasMaxLength(20);

            builder
               .HasMany(s => s.Students)
               .WithOne(sch => sch.School)
               .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
