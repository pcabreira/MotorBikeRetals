using MediatR;
using MotorBikeRetals.Core.Entities;
using System;

namespace MotorBikeRetals.Application.Queries.GetContractById
{
    public class GetContractByIdQuery : IRequest<Contract>
    {
        public GetContractByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
