using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveAllocationRepository(HrDatabaseContext context)
    : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
{
    private readonly HrDatabaseContext _context = context;

    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .Where(x => x.Id == id)
            .Include(x => x.LeaveType)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        return await _context.LeaveAllocations
            .Where(x => x.EmployeeId == userId)
            .Include(x => x.LeaveType)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
            .FirstOrDefaultAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(x =>
            x.EmployeeId == userId &&
            x.LeaveTypeId == leaveTypeId &&
            x.Period == period);
    }
}