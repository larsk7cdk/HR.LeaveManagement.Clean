using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler(
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validate the incoming data
        var validator = new UpdateLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
            throw new BadRequestException("Invalid Leave Type", validationResult);
        }

        var leaveTypeToUpdate = await leaveTypeRepository.GetByIdAsync(request.Id);

        if (leaveTypeToUpdate is null)
            throw new NotFoundException(nameof(LeaveType), request.Id);

        // Convert to domain entity object
        mapper.Map(request, leaveTypeToUpdate);

        // Update to database
        await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        // Return void
        return Unit.Value;
    }
}