using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Dtos;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;

namespace StudentsProblem.Controllers
{
    
    [ApiController]
    [Route("/professor")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorRepository professorRepository;
        private readonly ApplicationDbContext context;

        public ProfessorController(IProfessorRepository professorRepository, ApplicationDbContext context)
        {
            this.professorRepository = professorRepository;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allProfessors = await professorRepository.GetAllProfessorsAsync();
            return new JsonResult(allProfessors);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Details(int id)
        {
            var professor = await professorRepository.GetProfessorByIdAsync(id);
            return new JsonResult(professor);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProfessorCourseRequestDto pcrd)
        {
            var professor = new Professor()
            {
                Name = pcrd.ProfessorName
            };

            foreach (var course in pcrd.CourseIds)
            {
                professor.ProfessorCourses.Add(new ProfessorCourse()
                {
                    Professor = professor,
                    CourseId = course
                });
            }

            await professorRepository.AddProfessorAsync(professor);
            return Ok(professor);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, ProfessorCourseRequestDto pcrd)
        {
            var professor = await professorRepository.GetProfessorByIdAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            professor.Name = pcrd.ProfessorName;

            await professorRepository.UpdateProfessorAsync(professor, pcrd.CourseIds);

            context.Professors.Update(professor);
            return Ok(professor);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var profToDelete = await professorRepository.GetProfessorByIdAsync(id);
            if (profToDelete == null)
            {
                return NotFound();
            }

            await professorRepository.DeleteProfessorAsync(id);
            return Ok(profToDelete);
        }
    }
}
