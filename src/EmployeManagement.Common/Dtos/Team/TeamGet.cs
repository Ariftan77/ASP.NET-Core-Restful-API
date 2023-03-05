using EmployeeManagement.Common.Dtos.Employee;

namespace EmployeeManagement.Common.Dtos.Team;

public record TeamGet(int Id, string Name, List<EmployeeList> Employees);
