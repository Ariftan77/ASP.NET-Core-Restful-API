using EmployeeManagement.Common.Dtos.Address;

namespace EmployeeManagement.Common.Interfaces;

public interface IAddressService
{
    Task<int> CreateAddressAsync(AddressCreate addressCreate);
    Task UpdateAddressAsync(AddressUpdate addressUpdate);
    Task DeleteAddressAsync(AddressDelete addressDelete);
    Task<AddressGet> GetAddressByIdAsync(int id);
    Task<List<AddressGet>> GetAddressesAsync();
}
