using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Dtos.Team;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using FluentValidation;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Services;

public class TeamService : ITeamService
{
    private IMapper Mapper { get; }
    public IGenericRepository<Team> TeamRepository;
    public IGenericRepository<Employee> EmployeeRepository;
    private readonly TeamCreateValidator TeamCreateValidator;
    private readonly TeamUpdateValidator TeamUpdateValidator;

    public TeamService(IMapper mapper, IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository,
        TeamCreateValidator teamCreateValidator,
        TeamUpdateValidator teamUpdateValidator)
    {
        Mapper = mapper;
        TeamRepository = teamRepository;
        EmployeeRepository = employeeRepository;
        this.TeamCreateValidator = teamCreateValidator;
        this.TeamUpdateValidator = teamUpdateValidator;
    }

    public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
    {
        await TeamCreateValidator.ValidateAndThrowAsync(teamCreate);

        Expression<Func<Employee, bool>> employeeFilters = (emp) => teamCreate.Employees.Contains(emp.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilters }, null, null);
        
        var missingEmployee = teamCreate.Employees.Where((id) => !employees.Any(x => x.Id == id));

        if (missingEmployee.Any())
            throw new EmployeesNotFoundException(missingEmployee.ToArray());

        var entity = Mapper.Map<Team>(teamCreate);
        entity.Employees = employees;

        await TeamRepository.InsertAsync(entity);
        await TeamRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
    {
        await TeamUpdateValidator.ValidateAndThrowAsync(teamUpdate);

        Expression<Func<Employee, bool>> employeeFilters = (emp) => teamUpdate.Employees.Contains(emp.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilters }, null, null);

        var missingEmployees = teamUpdate.Employees.Where((id) => !employees.Any(x => x.Id == id));

        if (missingEmployees.Any())
            throw new EmployeesNotFoundException(missingEmployees.ToArray());

        var existingEntity = await TeamRepository.GetByIdAsync(teamUpdate.Id, includes: (team) => team.Employees);

        if (existingEntity == null)
            throw new EmployeeNotFoundException(teamUpdate.Id);

        Mapper.Map(teamUpdate, existingEntity);
        existingEntity.Employees = employees;

        TeamRepository.Update(existingEntity);
        await TeamRepository.SaveChangesAsync();
    }

    public async Task DeleteTeamAsync(TeamDelete teamDelete)
    {
        var entity = await TeamRepository.GetByIdAsync(teamDelete.Id);

        if (entity == null)
            throw new TeamNotFoundException(teamDelete.Id);

        TeamRepository.Delete(entity);
        await TeamRepository.SaveChangesAsync();
    }

    public async Task<TeamGet> GetTeamByIdAsync(int id)
    {
        var entity = await TeamRepository.GetByIdAsync(id,includes: (team) => team.Employees);

        if (entity == null)
            throw new TeamNotFoundException(id);

        return Mapper.Map<TeamGet>(entity);
    }   

    public async Task<List<TeamGet>> GetTeamsAsync()
    {
        var entities = await TeamRepository.GetAsync(null, null,includes: (team) => team.Employees);

        return Mapper.Map<List<TeamGet>>(entities);
    }
}
