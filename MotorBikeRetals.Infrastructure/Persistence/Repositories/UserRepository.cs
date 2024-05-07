using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorBikeRetals.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _mongoRepository;

        public UserRepository(IMongoRepository<User> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _mongoRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _mongoRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _mongoRepository.FindAsync(e=>e.Email == email && e.Password == passwordHash);
        }

        public async Task<User> AddAsync(User user)
        {
            await _mongoRepository.AddAsync(user);
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await _mongoRepository.UpdateAsync(user);
        }
    }
}
