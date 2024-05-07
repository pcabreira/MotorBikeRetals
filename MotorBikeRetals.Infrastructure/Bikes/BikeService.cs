using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Services;
using System.Text;
using System.Text.Json;

namespace MotorBikeRetals.Infrastructure.Bikes
{
    public class BikeService : IBikeService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "bike-created";

        public BikeService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessBikeCreate(Bike bikeInfo)
        {
            var bikeInfoJson = JsonSerializer.Serialize(bikeInfo);
            var bikeInfoBytes = Encoding.UTF8.GetBytes(bikeInfoJson);

            _messageBusService.Publish(QUEUE_NAME, bikeInfoBytes);
        }
    }
}
