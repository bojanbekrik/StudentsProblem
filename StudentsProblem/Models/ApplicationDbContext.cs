using Microsoft.EntityFrameworkCore;
using StudentsProblem.Migrations;
using System.Reflection;

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
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //applying every configuration with 1 line


            modelBuilder.ApplyConfiguration(new Configurations.StudentConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CourseConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ProfessorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StudentCourseConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.AddressConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ProfessorCourseConfiguration());

        }
    }
}
