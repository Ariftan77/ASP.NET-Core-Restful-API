

using AutoMapper;
using EmployeeManagement.Common.Dtos.Address;
using EmployeeManagement.Common.Dtos.Employee;
using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Dtos.Team;
using EmployeeManagement.Common.Model;

namespace EmployeeManagement.Business;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<AddressCreate, Address>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<AddressUpdate, Address>();
        CreateMap<Address, AddressGet>();

        CreateMap<JobCreate, Job>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<JobUpdate, Job>();
        CreateMap<Job, JobGet>();

        CreateMap<EmployeeCreate, Employee>().ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Job, opt => opt.Ignore())
            .ForMember(x => x.Teams, opt => opt.Ignore());

        CreateMap<EmployeeUpdate, Employee>().ForMember(x => x.Job, opt => opt.Ignore())
            .ForMember(x => x.Teams, opt => opt.Ignore());

        CreateMap<Employee, EmployeeDetails>().ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Job, opt => opt.Ignore())
            .ForMember(x => x.Address, opt => opt.Ignore());

        CreateMap<Employee, EmployeeList>();

        CreateMap<TeamCreate, Team>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Employees, opt => opt.Ignore());
        CreateMap<TeamUpdate, Team>()
            .ForMember(x => x.Employees, opt => opt.Ignore());
        CreateMap<Team, TeamGet>();
    }
}
