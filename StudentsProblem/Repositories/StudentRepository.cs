using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;
using System.IO.Pipelines;
using System.Linq;

namespace StudentsProblem.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await context.Students.Include(sc => sc.StudentCourses)
                .ThenInclude(c => c.Course)
                .Include(sch => sch.School)
                .OrderBy(s => s.StudentId)
                .ToListAsync();
        }

        public async Task<int> AddStudentAsync(Student student)
        {
            context.Students.Add(student);
            return await context.SaveChangesAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await context.Students
                .Include(sc => sc.StudentCourses)
                .ThenInclude(c => c.Course)
                .Include(sch => sch.School)
                .FirstOrDefaultAsync(x => x.StudentId == id);
        }

        public async Task UpdateStudentAsync(Student student, IEnumerable<int> selectedCourseIds)
        {
            var existingIds = student.StudentCourses.Select(sc => sc.CourseId).ToList();
            var toAdd = selectedCourseIds.Except(existingIds).ToList();
            var toRemove = existingIds.Except(selectedCourseIds).ToList();

            student.StudentCourses.RemoveAll(sc => toRemove.Contains(sc.CourseId));

            foreach (var courseId in toAdd)
            {
                student.StudentCourses.Add(new StudentCourse()
                {
                    CourseId = courseId
                });
            }

            await context.SaveChangesAsync();
        }

        public async Task<int> DeleteStudentAsync(int id)
        {
            var studentToDelete = await context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if (studentToDelete != null)
            {
                context.Students.Remove(studentToDelete);

                return await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The student to delete can't be found.");
            }
        }

        public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchByNameOrSurname)
        {
            var students = from s in context.Students select s;

            if (searchByNameOrSurname == null)
            {
                throw new ArgumentException("The string query is null");
            }
            else
            {
                students = students.Where(s => s.Name.Contains(searchByNameOrSurname) || s.Surname.Contains(searchByNameOrSurname));
            }

            return await students.ToListAsync();
        }

        public async Task<int> GetAllStudentsCountAsync()
        {
            IQueryable<Student> allStudents = from s in context.Students select s;
            var count = await allStudents.CountAsync();

            return count;
        }

        public async Task<IEnumerable<Student>> GetStudentsPagedAsync(int? pageNumber, int pageSize)
        {
            IQueryable<Student> students = from s in context.Students select s;

            pageNumber ??= 1;

            students = students.Skip((pageNumber.Value-1) * pageSize).Take(pageSize);

            return await students.AsNoTracking().ToListAsync();
        }

        public async Task<Student> SearchStudentByIndeksAsync(int indeks)
        {
            var students = from s in context.Students select s;

            students = students.Where(s => s.Indeks.Equals(indeks));

            foreach (var student in students)
            {
                if (student.Indeks == indeks)
                {
                    return student;
                }
            }

            throw new ArgumentException("There is not a student with that indeks");
            
        }
    }
}

