namespace HR.LeaveManagement.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; init; }

    public DateTime? DateCreated { get; init; }

    public DateTime? DateModified { get; init; }
}