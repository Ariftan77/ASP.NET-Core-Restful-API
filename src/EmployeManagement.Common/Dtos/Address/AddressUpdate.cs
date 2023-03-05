namespace EmployeeManagement.Common.Dtos.Address;

public record AddressUpdate(int Id, string Street, string ZipCode, string City, string Email, string? Phone);
