using MediatR;
using MotorBikeRetals.Core.Entities;
using System;

namespace MotorBikeRetals.Application.Commands.CreateContract
{
    public class CreateContractCommand : IRequest<Unit>
    {
        public Guid IdPlan { get; set; }
        public Guid IdUser { get; set; }
        public string TypeCNHUser { get; set; }
        public Guid IdBike { get; set; }
        public decimal TotalCost { get; set; }
    }
}
