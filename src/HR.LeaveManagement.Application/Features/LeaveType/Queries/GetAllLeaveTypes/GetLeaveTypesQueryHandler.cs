using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,IAppLogger<GetLeaveTypesQueryHandler> logger)
    : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query the database to get the list of leave types
        var leaveTypes = await leaveTypeRepository.GetAllAsync();
        
        // Convert the list of leave types to a list of LeaveTypeDto
        var data = mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        // Return the list of LeaveTypeDto
        logger.LogInformation("Leave types were retrieved successfully");
        return data;
    }
}