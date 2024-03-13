using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsProblem.Dtos;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;
using StudentsProblem.Utilities;
using System.IO.Pipelines;

namespace StudentsProblem.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IAddressRepository addressRepository;
        private readonly ISchoolRepository schoolRepository;
        private readonly ApplicationDbContext context;
        private int pageSize = 2;

        public StudentController(IStudentRepository studentRepository, IAddressRepository addressRepository, ISchoolRepository schoolRepository, ApplicationDbContext context)
        {
            this.studentRepository = studentRepository;
            this.addressRepository = addressRepository;
            this.schoolRepository = schoolRepository;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await studentRepository.GetAllStudentsAsync();
            return new JsonResult(students);
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentCourseRequestDto scrq)
        {
            var student = new Student()
            {
                Indeks = scrq.Indeks,
                Name = scrq.Name,
                Surname = scrq.Surname,
                SchoolId = scrq.SchoolId,
                AddressId = scrq.AddressId
            };

            foreach (var course in scrq.CourseIds)
            {
                student.StudentCourses.Add(new StudentCourse()
                {
                    Student = student,
                    CourseId = course
                });
            }

            await studentRepository.AddStudentAsync(student);
            return Ok(student);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Details(int id)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);
            return new JsonResult(student);
        }

        [HttpPut("{id}/update")] 
        public async Task<IActionResult> Update(int id, StudentCourseRequestDto scrd)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = scrd.Name;
            student.Surname = scrd.Surname;
            student.Indeks = scrd.Indeks;

            student.School = await schoolRepository.GetSchoolByIdAsync(scrd.SchoolId);

            student.Address = await addressRepository.GetAddressByIdAsync(scrd.AddressId);

            await studentRepository.UpdateStudentAsync(student, scrd.CourseIds);

            context.Students.Update(student);
            return Ok(student);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                await studentRepository.DeleteStudentAsync(id);
                return Ok(student);
            }
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Search(string searchByNameOrSurname)
        {
            if (searchByNameOrSurname != null)
            {
                var students = await studentRepository.SearchStudentsAsync(searchByNameOrSurname);
                return Ok(students);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("/pagination")]
        public async Task<IActionResult> IndexPaging(int? pageNumber)
        {
            var students = await studentRepository.GetStudentsPagedAsync(pageNumber, pageSize);

            pageNumber ??= 1;

            var count = await studentRepository.GetAllStudentsCountAsync();

            return new JsonResult(new PaginatedList<Student>(students.ToList(), count, pageNumber.Value, pageSize));
            //page size mi e 2
            //vrakja po 2ca na strana
            //poso imam 11 momentalno ako probas strana 6 kje go vrati samo posledniot
        }

        [HttpGet("searchByIndeks/{indeks}")]
        public async Task<IActionResult> SearchByIndeks(int indeks)
        {
            var student = await studentRepository.SearchStudentByIndeksAsync(indeks);
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
