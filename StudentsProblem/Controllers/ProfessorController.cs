using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;

namespace StudentsProblem.Controllers
{
    
    [ApiController]
    [Route("/professor")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorRepository professorRepository;

        public ProfessorController(IProfessorRepository professorRepository)
        {
            this.professorRepository = professorRepository;
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
        public async Task<IActionResult> Add(Professor professor)
        {
            await professorRepository.AddProfessorAsync(professor);
            if (professor == null)
            {
                return NotFound();
            }
            return Ok(professor);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(Professor professor)
        {
            if (professor == null)
            {
                return NotFound();
            }

            await professorRepository.UpdateProfessorAsync(professor);
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
