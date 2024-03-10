using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Models;

namespace StudentsProblem.Controllers
{
    [ApiController]
    [Route("school")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository schoolRepository;
        private readonly IAddressRepository addressRepository;

        public SchoolController(ISchoolRepository schoolRepository, IAddressRepository addressRepository)
        {
            this.schoolRepository = schoolRepository;
            this.addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var schools = await schoolRepository.GetAllSchoolsAsync();
            return new JsonResult(schools);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Details(int id)
        {
            var school = await schoolRepository.GetSchoolByIdAsync(id);
            if(school == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(school);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(SchoolAddressRequestDto sard)
        {
            var sch = new School()
            {
                Name = sard.Name,
                AddressId = sard.AddressId,
            };

            await schoolRepository.AddSchoolAsync(sch);
            return Ok(sch);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(SchoolAddressRequestDto sard, int id)
        {
            var sch = await schoolRepository.GetSchoolByIdAsync(id);
            if (sch == null)
            {
                return NotFound();
            }
            else
            {
                sch.Name = sard.Name;
                sch.AddressId = sard.AddressId;
            }

            sch.Address = await addressRepository.GetAddressByIdAsync(sch.AddressId);

            await schoolRepository.UpdateSchoolAsync(sch);
            return Ok(sch);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var schToDelete = await schoolRepository.GetSchoolByIdAsync(id);
            if (schToDelete == null)
            {
                return NotFound();
            }
            else
            {
                await schoolRepository.DeleteSchoolAsync(id);
                return Ok(schToDelete);
            }
        }

    }
}
