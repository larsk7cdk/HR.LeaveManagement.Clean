using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<CreateLeaveTypeCommand, int>
{
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validate the incoming data
        // Skal denne via DI?
        var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Type", validationResult);

        // Convert to domain entity object
        var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);

        // Add to database
        var leaveTypeCreated = await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        // Return record id
        return leaveTypeCreated.Id;
    }
}