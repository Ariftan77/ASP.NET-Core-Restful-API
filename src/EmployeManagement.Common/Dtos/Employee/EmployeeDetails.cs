using EmployeeManagement.Common.Dtos.Address;
using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Dtos.Team;

namespace EmployeeManagement.Common.Dtos.Employee;
public record EmployeeDetails (int Id, string FirstName, string LastName, AddressGet Address, JobGet Job, List<TeamGet> Teams);
