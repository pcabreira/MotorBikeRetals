using DevFreela.Application.Commands.DeleteBike;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRetals.Application.Commands.CreateBike;
using MotorBikeRetals.Application.Commands.UpdateBike;
using MotorBikeRetals.Application.Queries.GetAllBikes;
using MotorBikeRetals.Application.Queries.GetBikeById;
using MotorBikeRetals.Application.Queries.GetBikeByPlate;
using System;
using System.Threading.Tasks;

namespace MotorBikeRetals.API.Controllers
{
    [Route("api/bikes")]
    [Authorize]
    public class BikesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BikesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //api/bikes
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var getAllBikesQuery = new GetAllBikesQuery();
            var bikes = await _mediator.Send(getAllBikesQuery);

            if (bikes == null)
                return NotFound();

            return Ok(bikes);
        }

        //api/bikes/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetBikeByIdQuery(id);
            var bike = await _mediator.Send(query);

            if (bike == null)
                return NotFound();

            return Ok(bike);
        }

        //api/bikes/{plate}
        [HttpGet("{plate}/plate")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetByPlate(string plate)
        {
            var query = new GetBikeByPlateQuery(plate);
            var bike = await _mediator.Send(query);

            if (bike == null)
                return NotFound();

            return Ok(bike);
        }

        //api/bikes
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] CreateBikeCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        //api/bikes/update
        [HttpPatch("update")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Patch([FromBody] UpdateBikeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        //api/bikes/{id}/delete
        [HttpDelete("{id}/delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBikeCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
