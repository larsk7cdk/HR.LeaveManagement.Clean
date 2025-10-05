using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandValidator : AbstractValidator<DeleteLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;


    public DeleteLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;


        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExistsAsync);
    }

    private async Task<bool> LeaveAllocationMustExistsAsync(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveAllocationRepository.GetByIdAsync(id);
        return leaveType is not null;
    }
}