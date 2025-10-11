using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequest = mapper.Map<LeaveRequestDetailsDto>(
            await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));

        // Add employee details as needed

        return leaveRequest;
    }
}