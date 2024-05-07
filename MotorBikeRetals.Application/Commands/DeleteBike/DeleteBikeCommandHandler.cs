using MediatR;
using Microsoft.Extensions.Logging;
using MotorBikeRetals.Application.Commands.UpdateBike;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.DeleteBike
{
    public class DeleteBikeCommandHandler : IRequestHandler<DeleteBikeCommand, Unit>
    {
        private readonly IBikeRepository _repository;
        private readonly IContractRepository _contractrepository;
        private readonly ILogger<DeleteBikeCommand> _ILogger;

        public DeleteBikeCommandHandler(IBikeRepository repository, 
                                        IContractRepository contractrepository, 
                                        ILogger<DeleteBikeCommand> iLogger)
        {
            _repository = repository;
            _contractrepository = contractrepository;
            _ILogger = iLogger;
        }

        public async Task<Unit> Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
        {
            _ILogger.LogInformation($"Starting delete motorcycle...");

            try
            {
                var listContracts = await _contractrepository.GetAllAsync();

                if (!listContracts.Where(b => b.IdBike.Equals(request.Id)).Any())
                {
                    await _repository.DeleteAsync(request.Id);
                    _ILogger.LogInformation($"Motorcycle successfully deleted!");
                }
            }
            catch (Exception ex)
            {
                _ILogger.LogError($"Erro: {ex.Message}");
            }

            return Unit.Value;
        }
    }
}
