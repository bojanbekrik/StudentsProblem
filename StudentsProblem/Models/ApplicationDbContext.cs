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

        public DbSet<Address> Address { get; set; }

        public DbSet<School> School { get; set; }

        public DbSet<Professor> Professors { get; set; }

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
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<StudentCourse>()
                .HasIndex(sc => sc.Id)
                .IsUnique();

            modelBuilder.Entity<School>()
                .HasMany(s => s.Students)
                .WithOne(sch => sch.School)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Professor>()
                .HasMany(pc => pc.ProfessorCourses)
                .WithOne(p => p.Professor)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Course>()
                .HasMany(pc => pc.ProfessorCourses)
                .WithOne(c => c.Course)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ProfessorCourse>()
                .HasIndex(pc => pc.Id)
                .IsUnique();

            //adresa so skolo one to one
            modelBuilder.Entity<Address>()
                .HasOne(sch => sch.School)
                .WithOne(a => a.Address)
                .OnDelete(DeleteBehavior.ClientCascade);

            //adresa so student one to one
            modelBuilder.Entity<Address>()
                .HasOne(stu => stu.Student)
                .WithOne(a => a.Address)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
