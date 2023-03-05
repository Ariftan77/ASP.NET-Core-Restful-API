
using EmployeeManagement.Business.Services;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Business;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITeamService, TeamService>();

        services.AddScoped<AddressCreateValidator>();
        services.AddScoped<AddressUpdateValidator>();
        services.AddScoped<JobCreateValidator>();
        services.AddScoped<JobUpdateValidator>();
        services.AddScoped<EmployeeCreateValidator>();
        services.AddScoped<EmployeeUpdateValidator>();
        services.AddScoped<TeamCreateValidator>();
        services.AddScoped<TeamUpdateValidator>();
    }
}
