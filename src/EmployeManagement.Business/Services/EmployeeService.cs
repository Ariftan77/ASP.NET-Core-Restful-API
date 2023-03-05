using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Dtos.Employee;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using FluentValidation;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Services;


public class EmployeeService : IEmployeeService
{
    private IMapper Mapper { get; }
    public IGenericRepository<Employee> EmployeeRepository { get; }
    public IGenericRepository<Job> JobRepository { get; }
    public IGenericRepository<Address> AddressRepository { get; }
    public EmployeeCreateValidator EmployeeCreateValidator { get; }
    public EmployeeUpdateValidator EmployeeUpdateValidator { get; }

    public EmployeeService(IMapper mapper, IGenericRepository<Employee> employeeRepository,
        IGenericRepository<Job> jobRepository,
        IGenericRepository<Address> addressRepository,
        EmployeeCreateValidator employeeCreateValidator,
        EmployeeUpdateValidator employeeUpdateValidator)
    {
        Mapper = mapper;
        EmployeeRepository = employeeRepository;
        JobRepository = jobRepository;
        AddressRepository = addressRepository;
        EmployeeCreateValidator = employeeCreateValidator;
        EmployeeUpdateValidator = employeeUpdateValidator;
    }
    public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
    {
        await EmployeeCreateValidator.ValidateAndThrowAsync(employeeCreate);

        var job = await JobRepository.GetByIdAsync(employeeCreate.JobId);
        var address = await AddressRepository.GetByIdAsync(employeeCreate.AddressId);

        if (job == null)
            throw new JobNotFoundException(employeeCreate.JobId);
        if (address == null)
            throw new AddressNotFoundException(employeeCreate.AddressId);

        var entity = Mapper.Map<Employee>(employeeCreate);
        entity.Job = job!;
        entity.Address = address!;

        await EmployeeRepository.InsertAsync(entity);
        await EmployeeRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
    {
        var entity = await EmployeeRepository.GetByIdAsync(employeeDelete.Id);

        if (entity == null)
            throw new EmployeeNotFoundException(employeeDelete.Id);

        EmployeeRepository.Delete(entity);
        await EmployeeRepository.SaveChangesAsync();
    }

    public async Task<EmployeeDetails> GetEmployeeByIdAsync(int id)
    {
        var entity = await EmployeeRepository.GetByIdAsync(id,includes: new Expression<Func<Employee, object>>[] { (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams });

        if (entity == null)
            throw new EmployeeNotFoundException(id);

        return Mapper.Map<EmployeeDetails>(entity);
    }

    public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
    {
        await EmployeeUpdateValidator.ValidateAndThrowAsync(employeeUpdate);

        var job = await JobRepository.GetByIdAsync(employeeUpdate.JobId);
        var address = await AddressRepository.GetByIdAsync(employeeUpdate.AddressId);

        if (job == null)
            throw new JobNotFoundException(employeeUpdate.JobId);
        if (address == null)
            throw new AddressNotFoundException(employeeUpdate.AddressId);

        var existingEntity = await EmployeeRepository.GetByIdAsync(employeeUpdate.Id);

        if (existingEntity == null)
            throw new EmployeeNotFoundException(employeeUpdate.Id);

        var entity = Mapper.Map<Employee>(employeeUpdate);
        entity.Job = job!;
        entity.Address = address!;

        EmployeeRepository.Update(entity);
        await EmployeeRepository.SaveChangesAsync();
    }

    public async Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter)
    {
        Expression<Func<Employee, bool>> firstNameFilter = (employee) => employeeFilter.FirstName == null ? true : employee.FirstName.StartsWith(employeeFilter.FirstName);
        Expression<Func<Employee, bool>> lastNameFilter = (employee) => employeeFilter.LastName == null ? true : employee.LastName.StartsWith(employeeFilter.LastName);
        Expression<Func<Employee, bool>> jobFilter = (employee) => employeeFilter.Job == null ? true : employee.Job.Name.StartsWith(employeeFilter.Job);
        Expression<Func<Employee, bool>> teamFilter = (employee) => employeeFilter.Team == null ? true : employee.Teams.Any(team => team.Name.StartsWith(employeeFilter.Team));

        var entities = await EmployeeRepository.GetFilteredAsync(new Expression<Func<Employee, bool>>[]
        {
            firstNameFilter, lastNameFilter, jobFilter, teamFilter
        }, employeeFilter.Skip, employeeFilter.Take, includes: new Expression<Func<Employee, object>>[] {
        (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams });

        return Mapper.Map<List<EmployeeList>>(entities);
    }
}

