using MediatR;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Queries.GetBikeById
{
    public class GetBikeByIdQueryHandler : IRequestHandler<GetBikeByIdQuery, Bike>
    {
        private readonly IBikeRepository _repository;
        public GetBikeByIdQueryHandler(IBikeRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task<Bike> Handle(GetBikeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
