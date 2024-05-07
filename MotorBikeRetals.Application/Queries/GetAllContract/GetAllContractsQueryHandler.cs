using MediatR;
using MotorBikeRetals.Application.ViewModels;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Queries.GetAllContract
{
    public class GetAllContractsQueryHandler : IRequestHandler<GetAllContractsQuery, List<Contract>>
    {
        private readonly IContractRepository _contractRepository;
        public GetAllContractsQueryHandler(IContractRepository rentalRepository)
        {
            _contractRepository = rentalRepository;
        }

        public async Task<List<Contract>> Handle(GetAllContractsQuery request, CancellationToken cancellationToken)
        {
            return await _contractRepository.GetAllAsync();
        }
    }
}
