using MediatR;
using MotorBikeRetals.Core.Entities;
using System;

namespace MotorBikeRetals.Application.Queries.GetBikeById
{
    public class GetBikeByIdQuery : IRequest<Bike>
    {
        public GetBikeByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
