using StudentsProblem.Models;

namespace StudentsProblem.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAddressesAsync();

        Task<Address>? GetAddressByIdAsync(int id);

        Task<int> AddAddressAsync(Address address);

        Task<int> UpdateAddressAsync(Address address);

        Task<int> DeleteAddressAsync(int id);

        Task<int> GetAllAddressesCountAsync();

        Task<IEnumerable<Address>> GetAddressesPagedAsync(int? pageNumber, int pageSize);
    }
}
