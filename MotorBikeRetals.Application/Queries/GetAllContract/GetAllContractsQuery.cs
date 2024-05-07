using MediatR;
using MotorBikeRetals.Core.Entities;
using System.Collections.Generic;

namespace MotorBikeRetals.Application.Queries.GetAllContract
{
    public class GetAllContractsQuery : IRequest<List<Contract>>
    {
        public GetAllContractsQuery()
        {

        }
    }
}
