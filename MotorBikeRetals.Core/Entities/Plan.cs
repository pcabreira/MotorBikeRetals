using System;

namespace MotorBikeRetals.Core.Entities
{
    public class Plan : BaseEntity
    {
        public Plan(string description, int days, decimal cost, decimal totalCost)
        {
            Description = description;
            Days = days;
            Cost = cost;
            TotalCost = totalCost;  
        }

        public string Description { get; private set; }
        public int Days { get; private set; }
        public decimal Cost { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public void Update(string description, int days, decimal cost, decimal totalCost)
        {
            Description = description;
            Days = days;
            Cost = cost;
            TotalCost = totalCost;
        }
    }
}
