using Microsoft.Extensions.Configuration;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorBikeRetals.Infrastructure.Persistence.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly IMongoRepository<Contract> _mongoRepository;

        public ContractRepository(IMongoRepository<Contract> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<List<Contract>> GetAllAsync()
        {
            return await _mongoRepository.GetAllAsync();
        }

        public async Task<Contract> GetByIdAsync(Guid id)
        {
            return await _mongoRepository.GetByIdAsync(id);
        }

        public async Task<Contract> AddAsync(Contract contract)
        {
            return await _mongoRepository.AddAsync(contract);
        }

        public async Task UpdateAsync(Contract contract)
        {
            await _mongoRepository.UpdateAsync(contract);
        }
    }
}
