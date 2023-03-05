namespace EmployeeManagement.Common.Model;

public class Address : BaseEntity
{
    public string Street { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; }
    public List<Employee> Employees { get; set; } = default!;
}
