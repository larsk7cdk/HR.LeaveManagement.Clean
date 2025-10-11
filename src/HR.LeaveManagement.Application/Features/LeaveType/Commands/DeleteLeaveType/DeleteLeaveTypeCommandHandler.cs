using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler(
    ILeaveTypeRepository leaveTypeRepository,
    IAppLogger<DeleteLeaveTypeCommandHandler> logger)
    : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validate the incoming data
        var validator = new DeleteLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
            throw new BadRequestException("Invalid Leave ID", validationResult);
        }

        // Retrieve entity from database
        var leaveTypeToDelete = await leaveTypeRepository.GetByIdAsync(request.Id);

        // Verify that the entity exists
        if (leaveTypeToDelete == null)
            throw new NotFoundException(nameof(LeaveType), request.Id);


        // Delete from database
        await leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        // Return void
        return Unit.Value;
    }
}