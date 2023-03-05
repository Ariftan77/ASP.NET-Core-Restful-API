

using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Dtos.Address;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using FluentValidation;

namespace EmployeeManagement.Business.Services;

public class AddressService : IAddressService
{
    private IMapper Mapper { get; }
    public IGenericRepository<Address> AddressRepository { get; }
    public AddressCreateValidator AddressCreateValidator { get; }
    public AddressUpdateValidator AdressUpdateValidator { get; }

    public AddressService(IMapper mapper, IGenericRepository<Address> addressRepository, 
        AddressCreateValidator addressCreateValidator, 
        AddressUpdateValidator adressUpdateValidator)
    {
        Mapper = mapper;
        AddressRepository = addressRepository;
        AddressCreateValidator = addressCreateValidator;
        AdressUpdateValidator = adressUpdateValidator;
    }


    public async Task<int> CreateAddressAsync(AddressCreate addressCreate)
    {
        await AddressCreateValidator.ValidateAndThrowAsync(addressCreate);

        var entity = Mapper.Map<Address>(addressCreate);
        
        await AddressRepository.InsertAsync(entity);
        await AddressRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAddressAsync(AddressDelete addressDelete)
    {
        var entity = await AddressRepository.GetByIdAsync(addressDelete.id, includes: (address) => address.Employees);

        if (entity == null)
            throw new AddressNotFoundException(addressDelete.id);

        if (entity.Employees.Count > 0)
            throw new DependentEmployeeExistException(entity.Employees);

        AddressRepository.Delete(entity);
        await AddressRepository.SaveChangesAsync();
    }


    public async Task<AddressGet> GetAddressByIdAsync(int id)
    {
        var entity = await AddressRepository.GetByIdAsync(id);

        if (entity == null)
            throw new AddressNotFoundException(id);

        return Mapper.Map<AddressGet>(entity);
    }

    public async Task<List<AddressGet>> GetAddressesAsync()
    {
        var entities = await AddressRepository.GetAsync(null, null);
        return Mapper.Map<List<AddressGet>>(entities);
    }

    public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
    {
        await AdressUpdateValidator.ValidateAndThrowAsync(addressUpdate);

        var existingEntity = await AddressRepository.GetByIdAsync(addressUpdate.Id);

        if (existingEntity == null)
            throw new AddressNotFoundException(addressUpdate.Id);

        var entity = Mapper.Map<Address>(addressUpdate);

        AddressRepository.Update(entity);
        await AddressRepository.SaveChangesAsync();

    }
}
