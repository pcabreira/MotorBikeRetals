using MediatR;
using MotorBikeRetals.Core.Entities;
using System;

namespace MotorBikeRetals.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public UserDetails Details { get; set; }
    }
}
