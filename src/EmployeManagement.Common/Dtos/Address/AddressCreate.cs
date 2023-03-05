namespace EmployeeManagement.Common.Dtos.Address;

public record AddressCreate(string Street, string ZipCode, string City, string Email, string? Phone);
