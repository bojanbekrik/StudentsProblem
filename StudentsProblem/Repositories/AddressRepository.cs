using Microsoft.EntityFrameworkCore;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;

namespace StudentsProblem.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext context;

        public AddressRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await context.Address
                .Include(sch => sch.School)
                .OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<Address>? GetAddressByIdAsync(int id)
        {
            var addressToFind = await context.Address
                .Include(sch => sch.School)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (addressToFind == null)
            {
                throw new ArgumentException("Can not find that address");
            }
            else
            {
                return addressToFind;
            }
        }

        public async Task<int> AddAddressAsync(Address address)
        {
            context.Address.Add(address);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateAddressAsync(Address address)
        {
            context.Address.Update(address);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAddressAsync(int id)
        {
            var addressToDelete = await context.Address.FirstOrDefaultAsync(x => x.Id == id);
            if (addressToDelete == null)
            {
                throw new ArgumentException("Address to delete can not be found");
            }
            else
            {
                context.Address.Remove(addressToDelete);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> GetAllAddressesCountAsync()
        {
            IQueryable<Address> allAddresses = from a in context.Address select a;

            var count = await allAddresses.CountAsync();

            return count;
        }

        public async Task<IEnumerable<Address>> GetAddressesPagedAsync(int? pageNumber, int pageSize)
        {
            IQueryable<Address> addresses = from a in context.Address select a;

            pageNumber ??= 1;

            addresses = addresses.Skip((pageNumber.Value-1) * pageSize).Take(pageSize);

            return await addresses.AsNoTracking().ToListAsync();
        }

    }
}

