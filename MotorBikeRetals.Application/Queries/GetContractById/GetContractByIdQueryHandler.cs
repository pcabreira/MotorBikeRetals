using MediatR;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Queries.GetContractById
{
    public class GetContractByIdQueryHandler : IRequestHandler<GetContractByIdQuery, Contract>
    {
        private readonly IContractRepository _repository;
        public GetContractByIdQueryHandler(IContractRepository contractRepository)
        {
            _repository = contractRepository;
        }

        public async Task<Contract> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
