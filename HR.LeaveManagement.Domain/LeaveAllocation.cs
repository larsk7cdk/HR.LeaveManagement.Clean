using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation : BaseEntity
{
    public string EmployeeId { get; init; } = string.Empty;
    public int LeaveTypeId { get; init; }
    public LeaveType? LeaveType { get; init; }
    public int NumberOfDays { get; init; }
    public int Period { get; init; }
}