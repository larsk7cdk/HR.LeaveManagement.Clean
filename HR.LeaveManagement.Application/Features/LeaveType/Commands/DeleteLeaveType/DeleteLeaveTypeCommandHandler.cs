using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
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