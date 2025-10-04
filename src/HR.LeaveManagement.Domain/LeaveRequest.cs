using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveRequest : BaseEntity
{
    public int LeaveTypeId { get; init; }

    public LeaveType? LeaveType { get; init; }

    public DateTime DateRequested { get; init; }

    public string RequestComments { get; init; } = string.Empty;

    public bool? Approved { get; init; }

    public bool Cancelled { get; init; }

    public string RequestingEmployeeId { get; set; } = String.Empty;
}