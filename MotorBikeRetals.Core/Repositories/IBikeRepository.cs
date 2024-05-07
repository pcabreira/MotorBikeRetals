using MotorBikeRetals.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorBikeRetals.Core.Repositories
{
    public interface IBikeRepository
    {
        Task<List<Bike>> GetAllAsync();
        Task<Bike> GetByIdAsync(Guid id);
        Task AddAsync(Bike bike);
        Task UpdateAsync(Bike bike);
        Task DeleteAsync(Guid id);
    }
}
