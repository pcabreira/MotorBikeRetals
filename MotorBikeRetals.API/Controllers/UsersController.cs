﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorBikeRetals.Application.Commands.CreateUser;
using MotorBikeRetals.Application.Commands.CreateUserImage;
using MotorBikeRetals.Application.Commands.LoginUser;
using MotorBikeRetals.Application.Queries.GetUserById;
using System;
using System.Threading.Tasks;

namespace MotorBikeRetals.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //api/users/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, biker")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        //api/users/image
        [HttpPut("image")]
        [Authorize(Roles = "admin, biker")]
        public async Task<IActionResult> CreateUserImageCommand([FromForm] CreateUserImageCommand command)
        {
            var userUpdated = await _mediator.Send(command);
            return Ok(userUpdated);
        }

        // api/users/login
        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginUserviewModel = await _mediator.Send(command);

            if (loginUserviewModel == null)
                return BadRequest();

            return Ok(loginUserviewModel);
        }
    }
}
