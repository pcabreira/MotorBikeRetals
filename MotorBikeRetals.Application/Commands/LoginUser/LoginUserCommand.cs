﻿using MediatR;
using MotorBikeRetals.Application.ViewModels;

namespace MotorBikeRetals.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
