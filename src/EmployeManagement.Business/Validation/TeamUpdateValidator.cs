using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Dtos.Team;
using FluentValidation;

namespace EmployeeManagement.Business.Validation;

public class TeamUpdateValidator : AbstractValidator<TeamUpdate>
{
    public TeamUpdateValidator()
    {
        RuleFor(teamUpdate => teamUpdate.Name).NotEmpty().MaximumLength(50);
    }
}
