using EmployeeManagement.Common.Dtos.Address;
using FluentValidation;

namespace EmployeeManagement.Business.Validation;

public class AddressUpdateValidator : AbstractValidator<AddressUpdate>
{
    public AddressUpdateValidator()
    {
        RuleFor(addressUpdate => addressUpdate.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(addressUpdate => addressUpdate.City).NotEmpty().MaximumLength(100);
        RuleFor(addressUpdate => addressUpdate.Street).NotEmpty().MaximumLength(100);
        RuleFor(addressUpdate => addressUpdate.ZipCode).NotEmpty().MaximumLength(16);
        RuleFor(addressUpdate => addressUpdate.Phone).MaximumLength(32);

    }
}
