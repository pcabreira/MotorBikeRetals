using MediatR;
using Microsoft.Extensions.Logging;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Commands.FinishContract
{
    public class FinishContractCommandHandler : IRequestHandler<FinishContractCommand, Unit>
    {
        private readonly IContractRepository _contractRepository;
        private readonly ILogger<FinishContractCommand> _ILogger;

        public FinishContractCommandHandler(IContractRepository contractRepository, ILogger<FinishContractCommand> iLogger)
        {
            _contractRepository = contractRepository;
            _ILogger = iLogger;
        }

        public async Task<Unit> Handle(FinishContractCommand request, CancellationToken cancellationToken)
        {
            _ILogger.LogInformation($"Finishing contract...");

            try
            {
                var contract = await _contractRepository.GetByIdAsync(request.Id);
                contract.Finish();

                await _contractRepository.UpdateAsync(contract);

                _ILogger.LogInformation($"Contract finished!");
            }
            catch (Exception ex)
            {
                _ILogger.LogError($"Erro: {ex.Message}");
            }

            return Unit.Value;
        }
    }
}
