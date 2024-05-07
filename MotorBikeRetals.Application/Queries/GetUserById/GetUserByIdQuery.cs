using MediatR;
using MotorBikeRetals.Core.Entities;
using System;

namespace MotorBikeRetals.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
