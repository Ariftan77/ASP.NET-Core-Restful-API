using EmployeeManagement.Common.Dtos.Job;
using FluentValidation;

namespace EmployeeManagement.Business.Validation;

public class JobUpdateValidator : AbstractValidator<JobUpdate>
{
    public JobUpdateValidator()
    {
        RuleFor(jobUpdate => jobUpdate.Name).NotEmpty().MaximumLength(100);
        RuleFor(jobUpdate => jobUpdate.Description).NotEmpty().MaximumLength(250);
    }
}
