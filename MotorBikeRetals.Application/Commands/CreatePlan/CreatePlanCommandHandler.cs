using MediatR;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Commands.CreatePlan
{
    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, Unit>
    {
        private readonly IPlanRepository _planRepository;
        public CreatePlanCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Unit> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var totalCost = request.Days * request.Cost;
            var plan = new Plan(request.Description, request.Days, request.Cost, totalCost);

            await _planRepository.AddAsync(plan);

            return Unit.Value;
        }
    }
}
