using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
    IAppLogger<CreateLeaveTypeCommandHandler> logger)
    : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // Validate the incoming data
        // Skal denne via DI?
        var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            logger.LogWarning("Validation errors in update request for {0}", nameof(LeaveAllocation));
            throw new BadRequestException("Invalid Leave Allocation", validationResult);
        }

        // Get leave type
        var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
        
        // Convert to domain entity object
        var leaveAllocationToCreate = mapper.Map<Domain.LeaveAllocation>(request);

        // Add to database
        await leaveAllocationRepository.CreateAsync(leaveAllocationToCreate);

        // Return record id
        return leaveAllocationToCreate.Id;
    }
}