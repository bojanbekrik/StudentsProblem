using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;
using StudentsProblem.Utilities;

namespace StudentsProblem.Controllers
{

    [ApiController]
    [Route("/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        private int pageSize = 2;

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

        [HttpGet("/paginationOfAddresses")]
        public async Task<IActionResult> IndexPaging(int? pageNumber)
        {
            var addresses = await addressRepository.GetAddressesPagedAsync(pageNumber, pageSize);

            pageNumber ??= 1;

            var count = await addressRepository.GetAllAddressesCountAsync();

            return new JsonResult(new PaginatedList<Address>(addresses.ToList(), count, pageNumber.Value, pageSize));
        }

    }
}
