using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request,
        CancellationToken cancellationToken)
    {
        // Query the database to get the details
        var leaveType = await leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

        // Verify that the entity exists
        if (leaveType == null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);


        // Convert the list to a list of Dto
        var data = mapper.Map<LeaveAllocationDetailsDto>(leaveType);

        // Return the details
        return data;
    }
}