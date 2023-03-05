namespace EmployeeManagement.Common.Dtos.Team;

public record TeamUpdate(int Id, string Name, List<int> Employees);
