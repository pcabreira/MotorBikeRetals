using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MotorBikeRetals.Infrastructure.Persistence.Repositories
{
    public interface IMongoRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id); 
        Task<T> AddAsync(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}