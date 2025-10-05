using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<int>
{
    public string EmployeeId { get; init; } = string.Empty;
    public int LeaveTypeId { get; init; }
    public int NumberOfDays { get; init; }
    public int Period { get; init; }
}