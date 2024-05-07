namespace MotorBikeRetals.Core.Entities
{
    public class Bike : BaseEntity
    {
        public Bike(int year, string model, string plate)
        {
            Year = year;
            Model = model;
            Plate = plate;
        }

        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}
