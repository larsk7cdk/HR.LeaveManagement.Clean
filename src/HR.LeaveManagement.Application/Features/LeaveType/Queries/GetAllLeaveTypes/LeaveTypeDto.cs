namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveTypeDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public int DefaultDayse { get; init; }
}