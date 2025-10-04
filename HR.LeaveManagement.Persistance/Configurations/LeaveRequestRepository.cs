using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveRequestRepository(HrDatabaseContext context)
    : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{
    private readonly HrDatabaseContext _context = context;

    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);

        return leaveRequest;
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .ToListAsync();

        return leaveRequests;
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await _context.LeaveRequests
            .Where(x => x.RequestingEmployeeId == userId)
            .Include(x => x.LeaveType)
            .ToListAsync();

        return leaveRequests;
    }
}