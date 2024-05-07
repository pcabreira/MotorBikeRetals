using MediatR;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Queries.GetAllPlans
{
    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, List<Plan>>
    {
        private readonly IPlanRepository _repository;
        public GetAllPlansQueryHandler(IPlanRepository planRepository)
        {
            _repository = planRepository;
        }

        public async Task<List<Plan>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
