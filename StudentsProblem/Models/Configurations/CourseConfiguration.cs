using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace StudentsProblem.Models.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .Property(c => c.CourseName)
                .HasMaxLength(10);

            builder
                .HasMany(sc => sc.StudentCourses)
                .WithOne(c => c.Course)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(pc => pc.ProfessorCourses)
                .WithOne(c => c.Course)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
