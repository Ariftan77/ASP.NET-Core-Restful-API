using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Dtos.Team;
using FluentValidation;

namespace EmployeeManagement.Business.Validation;

public class TeamCreateValidator : AbstractValidator<TeamCreate>
{
    public TeamCreateValidator()
    {
        RuleFor(teamCreate => teamCreate.Name).NotEmpty().MaximumLength(50);
    }
}
