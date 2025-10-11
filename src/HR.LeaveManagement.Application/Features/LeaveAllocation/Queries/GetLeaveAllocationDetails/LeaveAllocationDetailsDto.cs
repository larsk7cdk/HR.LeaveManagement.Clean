using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class LeaveAllocationDetailsDto
{
    public int Id { get; init; }
    public int LeaveTypeId { get; init; }
    public LeaveTypeDto? LeaveType { get; init; }
    public int NumberOfDays { get; init; }
    public int Period { get; init; }
    public DateTime? DateCreated { get; init; }
    public DateTime? DateModified { get; init; }
}