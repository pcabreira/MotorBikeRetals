using MediatR;
using Microsoft.Extensions.Logging;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using MotorBikeRetals.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Commands.CreateBike
{
    public class CreateBikeCommandHandler : IRequestHandler<CreateBikeCommand, Unit>
    {
        private readonly IBikeRepository _repository;
        private readonly IBikeService _bikeService;
        private readonly ILogger<CreateBikeCommand> _ILogger;

        public CreateBikeCommandHandler(IBikeRepository repository, 
                                        IBikeService bikeService, 
                                        ILogger<CreateBikeCommand> iLogger)
        {
            _repository = repository;
            _bikeService = bikeService;
            _ILogger = iLogger;
        }

        public async Task<Unit> Handle(CreateBikeCommand request, CancellationToken cancellationToken)
        {
            _ILogger.LogInformation("Starting inclusion of a new motorcycle...");

            try
            {
                var bike = new Bike(request.Year, request.Model, request.Plate);
                await _repository.AddAsync(bike);

                _bikeService.ProcessBikeCreate(bike);

                _ILogger.LogInformation($"Motorcycle {bike.Model} successfully created!");
            }
            catch (Exception ex)
            {
                _ILogger.LogError($"Erro: {ex.Message}");
            }

            return Unit.Value;
        }
    }
}
