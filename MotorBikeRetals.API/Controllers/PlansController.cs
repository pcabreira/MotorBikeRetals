using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRetals.Application.Commands.CreatePlan;
using MotorBikeRetals.Application.Queries.GetAllPlans;
using MotorBikeRetals.Application.Queries.GetPlanById;
using System;
using System.Threading.Tasks;

namespace MotorBikeRetals.API.Controllers
{
    [Route("api/plans")]
    [Authorize]
    public class PlansController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/plans
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var getAllPlansQuery = new GetAllPlansQuery();
            var plans = await _mediator.Send(getAllPlansQuery); 

            return Ok(plans);
        }

        //api/plans/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetPlanByIdQuery(id);
            var plan = await _mediator.Send(query);

            if (plan == null)
                return NotFound();

            return Ok(plan);
        }

        //api/plans/create
        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] CreatePlanCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }
    }
}
