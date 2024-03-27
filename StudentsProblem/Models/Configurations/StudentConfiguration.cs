using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace StudentsProblem.Models.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasMany(sc => sc.StudentCourses)
                .WithOne(s => s.Student)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .Property(s => s.Name)
                .HasMaxLength(10);
        }
    }
}
