using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public int LeaveTypeId { get; init; }
    public int NumberOfDays { get; init; }
    public int Period { get; init; }
}