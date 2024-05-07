using MediatR;
using MotorBikeRetals.Core.Entities;
using System;

namespace MotorBikeRetals.Application.Commands.UpdateBike
{
    public class UpdateBikeCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Plate { get; set; }
    }
}
