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
                .WithOne(s => s.Student);

            modelBuilder.Entity<Course>()
                .HasMany(sc => sc.StudentCourses)
                .WithOne(c => c.Course);

            modelBuilder.Entity<StudentCourse>()
                .HasIndex(sc => sc.Id)
                .IsUnique();
                


            /*
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentsId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CoursesId);
            */
        }
    }
}
