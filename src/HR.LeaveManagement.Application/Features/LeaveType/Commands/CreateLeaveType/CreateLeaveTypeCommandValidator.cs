using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

        RuleFor(p => p.DefaultDays)
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than {ComparisonValue}")
            .LessThan(100).WithMessage("{PropertyName} cannot exceed {ComparisonValue}");

        RuleFor(p => p)
            .MustAsync(LeaveTypeNameUniqueAsync)
            .WithMessage("Leave type with the same name already exists.");
    }

    private async Task<bool> LeaveTypeNameUniqueAsync(CreateLeaveTypeCommand command,
        CancellationToken cancellationToken)
    {
        return !await _leaveTypeRepository.IsLeaveTypeUniqueAsync(command.Name);
    }
}