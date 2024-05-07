using MotorBikeRetals.Core.Entities;
using System.Threading.Tasks;

namespace MotorBikeRetals.Core.Repositories
{
    public interface IBikeNotificationsRepository
    {
        Task AddAsync(BikeNotification bikeNotification);
    }
}
