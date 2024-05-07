using MotorBikeRetals.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorBikeRetals.Core.Repositories
{
    public interface IPlanRepository
    {
        Task<List<Plan>> GetAllAsync();
        Task<Plan> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Plan plan);
    }
}
