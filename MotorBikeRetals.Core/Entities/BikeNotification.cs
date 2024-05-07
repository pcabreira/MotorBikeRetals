namespace MotorBikeRetals.Core.Entities
{
    public class BikeNotification : BaseEntity
    {
        public BikeNotification(string idBike, string description)
        {
           IdBike = idBike;
           Description = description;
        }

        public string IdBike { get; set; }
        public string Description { get; set; }
    }
}
