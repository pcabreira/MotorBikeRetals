using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using MotorBikeRetals.Core.Services;
using MotorBikeRetals.Infrastructure.Auth;
using MotorBikeRetals.Infrastructure.Bikes;
using MotorBikeRetals.Infrastructure.MessageBus;
using MotorBikeRetals.Infrastructure.Persistence;
using MotorBikeRetals.Infrastructure.Persistence.Repositories;

namespace MotorBikeRetals.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services) {
            services.AddSingleton(sp => {
                var configuration = sp.GetService<IConfiguration>();
                var options = new MongoDbOptions();

                configuration.GetSection("Mongo").Bind(options);

                return options;
            });

            services.AddSingleton<IMongoClient>(sp => {
                var options = sp.GetService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddTransient(sp => {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
                var options = sp.GetService<MongoDbOptions>();
                var mongoClient = sp.GetService<IMongoClient>();

                return mongoClient.GetDatabase(options.Database);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddMongoRepository<User>("users");
            services.AddMongoRepository<UserImage>("userImage");
            services.AddMongoRepository<Contract>("contracts");
            services.AddMongoRepository<Plan>("plans");
            services.AddMongoRepository<Bike>("bikes");
            services.AddMongoRepository<BikeNotification>("bikeNotifications");

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IBikeRepository, BikeRepository>();
            services.AddScoped<IBikeNotificationsRepository, BikeNotificationsRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBikeService, BikeService>();
            services.AddScoped<IMessageBusService, MessageBusService>();

            return services;
        }

        private static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collection) where T: BaseEntity
        {
            services.AddScoped<IMongoRepository<T>>(f => 
            {
                var mongoDatabase = f.GetRequiredService<IMongoDatabase>();
                return new MongoRepository<T>(mongoDatabase, collection);
            });

            return services;
        }
    }
}