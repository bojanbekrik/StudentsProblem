using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace StudentsProblem.Models.Configurations
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(10);

            builder
                .HasMany(pc => pc.ProfessorCourses)
                .WithOne(p => p.Professor)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
