using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System.Threading.Tasks;

namespace MotorBikeRetals.Infrastructure.Persistence.Repositories
{
    public class BikeNotificationsRepository : IBikeNotificationsRepository
    {
        private readonly IMongoRepository<BikeNotification> _mongoRepository;

        public BikeNotificationsRepository(IMongoRepository<BikeNotification> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task AddAsync(BikeNotification bikeNotification)
        {
            await _mongoRepository.AddAsync(bikeNotification);
        }
    }
}
