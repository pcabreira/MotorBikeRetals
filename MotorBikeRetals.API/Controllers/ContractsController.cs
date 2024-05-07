using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRetals.Application.Commands.CreateContract;
using MotorBikeRetals.Application.Commands.FinishContract;
using MotorBikeRetals.Application.Commands.StartContract;
using MotorBikeRetals.Application.Queries.GetAllContract;
using MotorBikeRetals.Application.Queries.GetContractById;
using System;
using System.Threading.Tasks;

namespace MotorBikeRetals.API.Controllers
{
    [Route("api/contracts")]
    [Authorize]
    public class ContractsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContractsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //api/contracts
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            var getAllContractsQuery = new GetAllContractsQuery();
            var contracts = await _mediator.Send(getAllContractsQuery);

            return Ok(contracts);
        }

        //api/contracts/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, biker")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetContractByIdQuery(id);
            var contract = await _mediator.Send(query);

            if (contract == null)
                return NotFound();

            return Ok(contract);
        }

        //api/contracts/{ReturnDate}/{id}/rentalvalue
        [HttpGet("{ReturnDate}/{id}/rentalvalue")]
        [Authorize(Roles = "admin, biker")]
        public async Task<IActionResult> GetRentalValue(Guid id, DateTime returnDate)
        {
            var query = new GetRentalValueByIdQuery(id, returnDate);
            var rentalValue = await _mediator.Send(query);

            return Ok(rentalValue);
        }

        //api/contracts
        [HttpPost]
        [Authorize(Roles = "admin, biker")]
        public async Task<IActionResult> Post([FromBody] CreateContractCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        //api/contracts/{id}/start
        [HttpPut("{id}/start")]
        [AllowAnonymous]
        public async Task<IActionResult> Start(Guid id)
        {
            var command = new StartContractCommand(id);
            await _mediator.Send(command);

            return Accepted();
        }

        //api/contracts/{id}/finish
        [HttpPut("{id}/finish")]
        [AllowAnonymous]
        public async Task<IActionResult> Finish(Guid id)
        {
            var command = new FinishContractCommand(id);
            await _mediator.Send(command);

            return Accepted();
        }
    }
}
