using MediatR;
using MotorBikeRetals.Core.Entities;
using System.Collections.Generic;

namespace MotorBikeRetals.Application.Queries.GetAllBikes
{
    public class GetAllBikesQuery : IRequest<List<Bike>>
    {
        public GetAllBikesQuery()
        {

        }
    }
}
