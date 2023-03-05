using EmployeeManagement.Common.Dtos.Employee;
using FluentValidation;

namespace EmployeeManagement.Business.Validation;

public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdate>
{
    public EmployeeUpdateValidator()
    {
        RuleFor(employeeUpdate => employeeUpdate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(employeeUpdate => employeeUpdate.LastName).NotEmpty().MaximumLength(50);
    }
}
