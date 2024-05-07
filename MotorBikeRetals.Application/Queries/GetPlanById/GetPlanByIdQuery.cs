using MediatR;
using MotorBikeRetals.Core.Entities;
using System;

namespace MotorBikeRetals.Application.Queries.GetPlanById
{
    public class GetPlanByIdQuery : IRequest<Plan>
    {
        public GetPlanByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
