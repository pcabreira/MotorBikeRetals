using MotorBikeRetals.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorBikeRetals.Core.Repositories
{
    public interface IContractRepository
    {
        Task<List<Contract>> GetAllAsync();
        Task<Contract> GetByIdAsync(Guid id);
        Task<Contract> AddAsync(Contract contract);
        Task UpdateAsync(Contract contract);
    }
}
