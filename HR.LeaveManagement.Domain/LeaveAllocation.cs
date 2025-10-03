using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; init; }

    public int LeaveTypeId { get; init; }

    public LeaveType? LeaveType { get; init; }

    public int Period { get; init; }
}