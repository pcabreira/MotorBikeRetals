using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorBikeRetals.Infrastructure.Persistence.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly IMongoRepository<Plan> _mongoRepository;

        public PlanRepository(IMongoRepository<Plan> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<List<Plan>> GetAllAsync()
        {
            return await _mongoRepository.GetAllAsync();
        }

        public async Task<Plan> GetByIdAsync(Guid id)
        {
            return await _mongoRepository.GetByIdAsync(id);
        }

        public async Task<Guid> AddAsync(Plan plan)
        {
            await _mongoRepository.AddAsync(plan);
            return plan.Id;
        }
    }
}
