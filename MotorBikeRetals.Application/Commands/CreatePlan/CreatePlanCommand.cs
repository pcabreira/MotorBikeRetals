using MediatR;
using System;

namespace MotorBikeRetals.Application.Commands.CreatePlan
{
    public class CreatePlanCommand : IRequest<Unit>
    {
        public string Description { get; set; }
        public int Days { get; set; }
        public decimal Cost { get; set; }
    }
}
