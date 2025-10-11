using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveTypeRepository(HrDatabaseContext context)
    : GenericRepository<LeaveType>(context), ILeaveTypeRepository
{
    private readonly HrDatabaseContext _context = context;

    public async Task<bool> IsLeaveTypeUniqueAsync(string name)
    {
        var result = await _context.LeaveTypes.AnyAsync(p => p.Name == name);
        return result;
    }
}