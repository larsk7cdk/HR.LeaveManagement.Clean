using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query the database to get the list of leave types
        var leaveTypes = await leaveTypeRepository.GetAllAsync();
        
        // Convert the list of leave types to a list of LeaveTypeDto
        var data = mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        // Return the list of LeaveTypeDto
        return data;
    }
}