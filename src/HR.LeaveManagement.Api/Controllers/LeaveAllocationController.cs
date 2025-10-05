using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesDefaultResponseType]
        [Produces("application/json")]
        public async Task<ActionResult<List<LeaveTypeDto>>> GetAll()
        {
            var leaveAllocations = await mediator.Send(new GetLeaveAllocationsQuery());
            return Ok(leaveAllocations);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> GetByAsync(int id)
        {
            var leaveType = await mediator.Send(new GetLeaveAllocationDetailsQuery(id));
            return Ok(leaveType);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] CreateLeaveAllocationCommand request)
        {
            var result = await mediator.Send(request);
            return CreatedAtAction(nameof(Post), new { id = result });
        }

        // PUT api/<LeaveTypesController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationCommand request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}