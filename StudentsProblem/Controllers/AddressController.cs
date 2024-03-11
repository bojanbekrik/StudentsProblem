using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;

namespace StudentsProblem.Controllers
{

    [ApiController]
    [Route("/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var addresses = await addressRepository.GetAllAddressesAsync();
            return new JsonResult(addresses);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Details(int id)
        {
            var address = await addressRepository.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            else
            {
                return new JsonResult(address);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Address address)
        {
            await addressRepository.AddAddressAsync(address);
            return Ok(address);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(Address address)
        {
            if (address == null)
            {
                return NotFound();
            }
            await addressRepository.UpdateAddressAsync(address);
            return Ok(address);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var addressToDelete = await addressRepository.GetAddressByIdAsync(id);
            if (addressToDelete == null)
            {
                return NotFound();
            }
            else
            {
                await addressRepository.DeleteAddressAsync(id);
                return Ok(addressToDelete);
            }
        }
    }   
}
