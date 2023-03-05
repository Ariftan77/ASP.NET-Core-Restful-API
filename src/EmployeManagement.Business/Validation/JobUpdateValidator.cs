using EmployeeManagement.Common.Dtos.Job;
using FluentValidation;

namespace EmployeeManagement.Business.Validation;

public class JobCreateValidator : AbstractValidator<JobCreate>
{
    public JobCreateValidator()
    {
        RuleFor(jobCreate => jobCreate.Name).NotEmpty().MaximumLength(100);
        RuleFor(jobCreate => jobCreate.Description).NotEmpty().MaximumLength(250);
    }
}
