using MotorBikeRetals.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorBikeRetals.Core.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAndPasswordAsync(string email, string passwordHash);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
