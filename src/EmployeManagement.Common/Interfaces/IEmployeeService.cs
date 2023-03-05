using EmployeeManagement.Common.Dtos.Employee;

namespace EmployeeManagement.Common.Interfaces;

public interface IEmployeeService
{
    Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate);
    Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);
    Task DeleteEmployeeAsync(EmployeeDelete employeeDelete);
    Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter);
    Task<EmployeeDetails> GetEmployeeByIdAsync(int id);
}
