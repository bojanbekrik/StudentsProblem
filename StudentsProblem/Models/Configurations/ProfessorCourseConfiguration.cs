using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace StudentsProblem.Models.Configurations
{
    public class ProfessorCourseConfiguration : IEntityTypeConfiguration<ProfessorCourse>
    {
        public void Configure(EntityTypeBuilder<ProfessorCourse> builder)
        {
            builder
                .HasIndex(pc => pc.Id)
                .IsUnique();
        }
    }
}
