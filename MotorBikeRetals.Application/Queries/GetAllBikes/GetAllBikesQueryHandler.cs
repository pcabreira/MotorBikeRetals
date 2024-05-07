using MediatR;
using MotorBikeRetals.Application.Queries.GetAllBikes;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Queries.GetAllPlans
{
    public class GetAllBikesQueryHandler : IRequestHandler<GetAllBikesQuery, List<Bike>>
    {
        private readonly IBikeRepository _bikeRepository;
        public GetAllBikesQueryHandler(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public async Task<List<Bike>> Handle(GetAllBikesQuery request, CancellationToken cancellationToken)
        {
            return await _bikeRepository.GetAllAsync();
        }
    }
}
