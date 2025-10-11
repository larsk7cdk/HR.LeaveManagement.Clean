using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler(
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
    IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
    : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // Validate the incoming data
        var validator = new UpdateLeaveAllocationCommandValidator(leaveTypeRepository, leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
            throw new BadRequestException("Invalid Leave Type", validationResult);
        }

        var leaveTypeToUpdate = await leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveTypeToUpdate is null)
            throw new NotFoundException(nameof(LeaveType), request.Id);

        // Convert to domain entity object
        mapper.Map(request, leaveTypeToUpdate);

        // Update to database
        await leaveAllocationRepository.UpdateAsync(leaveTypeToUpdate);

        // Return void
        return Unit.Value;
    }
}