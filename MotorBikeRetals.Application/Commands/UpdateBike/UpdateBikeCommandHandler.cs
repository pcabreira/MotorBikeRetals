using MediatR;
using Microsoft.Extensions.Logging;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Commands.UpdateBike
{
    public class UpdateBikeCommandHandler : IRequestHandler<UpdateBikeCommand, Unit>
    {
        private readonly IBikeRepository _repository;
        private readonly ILogger<UpdateBikeCommand> _ILogger;
        public UpdateBikeCommandHandler(IBikeRepository repository, ILogger<UpdateBikeCommand> iLogger)
        {
            _repository = repository;
            _ILogger = iLogger;
        }

        public async Task<Unit> Handle(UpdateBikeCommand request, CancellationToken cancellationToken)
        {
            _ILogger.LogInformation($"Starting update motorcycle...");

            try
            {
                var bike = await _repository.GetByIdAsync(request.Id);

                if (bike != null)
                {
                    bike = new Bike(bike.Year, bike.Model, request.Plate);
                    bike.Id = request.Id;

                    await _repository.UpdateAsync(bike);

                    _ILogger.LogInformation($"Motorcycle {bike.Model} successfully updated!");
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
