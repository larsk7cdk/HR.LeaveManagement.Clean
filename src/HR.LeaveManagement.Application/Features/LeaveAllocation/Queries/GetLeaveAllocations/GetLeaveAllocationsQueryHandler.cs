using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationsQueryHandler(
    IMapper mapper,
    ILeaveAllocationRepository leaveAllocationRepository,
    IAppLogger<GetLeaveAllocationsQueryHandler> logger)
    : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
{
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request,
        CancellationToken cancellationToken)
    {
        // Query the database to get a list 
        var leaveAllocations = await leaveAllocationRepository.GetLeaveAllocationsWithDetails();

        // Convert the list to a list of Dto
        var data = mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        // Return the list
        logger.LogInformation("Leave allocations were retrieved successfully");
        return data;
    }
}