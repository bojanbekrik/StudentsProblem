using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllAddressesAsync()
        {
            var addresses = await addressRepository.GetAllAddressesAsync();
            return new JsonResult(addresses);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetAddressByIdAsync(int id)
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
        public async Task<IActionResult> AddAddressAsync(Address address)
        {
            await addressRepository.AddAddressAsync(address);
            return Ok(address);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateAddressAsync(Address address)
        {
            if (address == null)
            {
                return NotFound();
            }
            await addressRepository.UpdateAddressAsync(address);
            return Ok(address);
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteAddressAsync(int id)
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
