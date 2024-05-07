using MediatR;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Commands.CreateContract
{
    public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, Unit>
    {
        private readonly IContractRepository _repository;
        private readonly IPlanRepository _repositoryPlan;
        public CreateContractCommandHandler(IContractRepository repository, IPlanRepository repositoryPlan)
        {
            _repository = repository;
            _repositoryPlan = repositoryPlan;
        }

        public async Task<Unit> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            var plan = _repositoryPlan.GetByIdAsync(request.IdPlan);

            var contract = new Contract(request.IdPlan,
                                        plan.Result.Days,
                                        request.IdUser, 
                                        request.TypeCNHUser, 
                                        request.IdBike, 
                                        request.TotalCost);

            await _repository.AddAsync(contract);

            return Unit.Value;
        }
    }
}
