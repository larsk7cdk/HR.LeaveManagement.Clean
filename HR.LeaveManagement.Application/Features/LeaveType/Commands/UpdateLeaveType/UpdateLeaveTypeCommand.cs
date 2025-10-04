using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public int DefaultDays { get; init; }
}