using Microsoft.EntityFrameworkCore;

namespace StudentsProblem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Student>()
                .HasMany(sc => sc.StudentCourses)
                .WithOne(s => s.Student)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Course>()
                .HasMany(sc => sc.StudentCourses)
                .WithOne(c => c.Course)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudentCourse>()
                .HasIndex(sc => sc.Id)
                .IsUnique();
        }
    }
}
