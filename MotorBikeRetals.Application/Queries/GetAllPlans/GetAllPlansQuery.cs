using MediatR;
using MotorBikeRetals.Core.Entities;
using System.Collections.Generic;

namespace MotorBikeRetals.Application.Queries.GetAllPlans
{
    public class GetAllPlansQuery : IRequest<List<Plan>>
    {
        public GetAllPlansQuery()
        {

        }
    }
}
