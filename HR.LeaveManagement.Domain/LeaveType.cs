using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveType : BaseEntity
{
    public string Name { get; init; } = string.Empty;

    public int DefaultDayse { get; init; }
}