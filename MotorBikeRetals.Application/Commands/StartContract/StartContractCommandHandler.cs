using MediatR;
using Microsoft.Extensions.Logging;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Commands.StartContract
{
    public class StartcontractCommandHandler : IRequestHandler<StartContractCommand, Unit>
    {
        private readonly IContractRepository _contractRepository;
        private readonly ILogger<StartContractCommand> _ILogger;
        public StartcontractCommandHandler(IContractRepository contractRepository, ILogger<StartContractCommand> iLogger)
        {
            _contractRepository = contractRepository;
            _ILogger = iLogger;
        }

        public async Task<Unit> Handle(StartContractCommand request, CancellationToken cancellationToken)
        {
            _ILogger.LogInformation($"Starting contract...");

            try
            {
                var contract = await _contractRepository.GetByIdAsync(request.Id);

                if (contract.StartedAt.Date == DateTime.Today)
                {
                    contract.Start();
                    _ILogger.LogInformation($"Contract started!");
                }
                else if (contract.StartedAt < DateTime.Now)
                {
                    contract.Cancel();
                    _ILogger.LogInformation($"Contract canceled!");
                }

                await _contractRepository.UpdateAsync(contract);
            }
            catch (Exception ex)
            {
                _ILogger.LogError($"Erro: {ex.Message}");
            }

            return Unit.Value;
        }
    }
}
