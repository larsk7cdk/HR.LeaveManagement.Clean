namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class LeaveAllocationDto
{
    public string EmployeeId { get; init; }
    public int LeaveTypeId { get; init; }
    public Domain.LeaveType? LeaveType { get; init; }
    public int NumberOfDays { get; init; }
    public int Period { get; init; }
}