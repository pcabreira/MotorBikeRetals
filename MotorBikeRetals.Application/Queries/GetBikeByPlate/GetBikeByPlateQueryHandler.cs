using MediatR;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Queries.GetBikeByPlate
{
    public class GetBikeByPlateQueryHandler : IRequestHandler<GetBikeByPlateQuery, List<Bike>>
    {
        private readonly IBikeRepository _repository;
        public GetBikeByPlateQueryHandler(IBikeRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task<List<Bike>> Handle(GetBikeByPlateQuery request, CancellationToken cancellationToken)
        {
            var listBikesFilter = new List<Bike>();
            var listBikes = await _repository.GetAllAsync();

            foreach (var bike in listBikes.Where(b => b.Plate.Equals(request.Plate)).ToList())
                listBikesFilter.Add(new Bike(bike.Year, bike.Plate, bike.Model));

            if (listBikesFilter == null)
                return null;

            return listBikesFilter;
        }
    }
}
