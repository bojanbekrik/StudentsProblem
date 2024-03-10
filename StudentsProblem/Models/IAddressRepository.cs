﻿namespace StudentsProblem.Models
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAddressesAsync();

        Task<Address>? GetAddressByIdAsync(int id);

        Task<int> AddAddressAsync(Address address);

        Task<int> UpdateAddressAsync(Address address);

        Task<int> DeleteAddressAsync(int id);

    }
}
