using MongoDB.Bson;
using MongoDB.Driver;
using MotorBikeRetals.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MotorBikeRetals.Infrastructure.Persistence.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T: BaseEntity
    {
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<T>(collectionName);
        }

        public IMongoCollection<T> Collection { get; private set; }

        public async Task<List<T>> GetAllAsync()
        {
            return await Collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Collection.FindAsync(e => e.Id.Equals(id)).Result.FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await Collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Collection.DeleteOneAsync(e => e.Id.Equals(id));
        }
    }
}