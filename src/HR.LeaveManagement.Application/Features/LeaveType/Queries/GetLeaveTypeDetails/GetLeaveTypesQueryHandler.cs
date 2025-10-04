using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query the database to get the list of leave types
        var leaveType = await leaveTypeRepository.GetByIdAsync(request.Id);

        // Verify that the entity exists
        if (leaveType == null)
            throw new NotFoundException(nameof(LeaveType), request.Id);


        // Convert the list of leave types to a list of LeaveTypeDto
        var data = mapper.Map<LeaveTypeDetailsDto>(leaveType);

        // Return the list of LeaveTypeDto
        return data;
    }
}