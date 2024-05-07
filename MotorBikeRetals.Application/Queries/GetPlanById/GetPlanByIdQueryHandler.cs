using MediatR;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Queries.GetPlanById
{
    public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, Plan>
    {
        private readonly IPlanRepository _repository;
        public GetPlanByIdQueryHandler(IPlanRepository planRepository)
        {
            _repository = planRepository;
        }

        public async Task<Plan> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
