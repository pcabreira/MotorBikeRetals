using MongoDB.Driver;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorBikeRetals.Infrastructure.Persistence.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly IMongoRepository<Bike> _mongoRepository;

        public BikeRepository(IMongoRepository<Bike> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<List<Bike>> GetAllAsync()
        {
            return await _mongoRepository.GetAllAsync();
        }

        public async Task<Bike> GetByIdAsync(Guid id)
        {
            return await _mongoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Bike bike)
        {
            await _mongoRepository.AddAsync(bike);
        }

        public async Task UpdateAsync(Bike bike)
        {
            await _mongoRepository.UpdateAsync(bike);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
