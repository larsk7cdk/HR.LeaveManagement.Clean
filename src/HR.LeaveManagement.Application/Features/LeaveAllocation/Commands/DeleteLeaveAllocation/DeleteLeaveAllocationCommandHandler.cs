using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    IAppLogger<DeleteLeaveAllocationCommandHandler> logger)
    : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // Validate the incoming data
        var validator = new DeleteLeaveAllocationCommandValidator(leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
            throw new BadRequestException("Invalid Leave ID", validationResult);
        }

        // Retrieve entity from database
        var leaveAllocationToDelete = await leaveAllocationRepository.GetByIdAsync(request.Id);

        // Verify that the entity exists
        if (leaveAllocationToDelete == null)
            throw new NotFoundException(nameof(LeaveType), request.Id);


        // Delete from database
        await leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);

        // Return void
        return Unit.Value;
    }
}