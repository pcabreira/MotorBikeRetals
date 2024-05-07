using MotorBikeRetals.Core.Enums;
using System;

namespace MotorBikeRetals.Core.Entities
{
    public class Contract : BaseEntity
    {
        public Contract(Guid idPlan,
                        int daysPlan,
                        Guid idUser,
                        string typeCNHUser,
                        Guid idBike, 
                        decimal totalCost)
        {
            IdPlan = idPlan;
            DaysPlan = daysPlan;
            IdUser = idUser;
            TypeCNHUser = typeCNHUser;
            IdBike = idBike;
            TotalCost = totalCost;    
            CreatedAt = DateTime.Today;
            StartedAt = DateTime.Today.AddDays(1);
            ExpectedFinish = DateTime.Today.AddDays(daysPlan);
            Status = ContractStatusEnum.Created;
        }

        public Guid IdPlan { get; private set; }
        public int DaysPlan { get; private set; }
        public Guid IdUser { get; private set; }
        public string TypeCNHUser { get; set; }
        public Guid IdBike { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime ExpectedFinish { get; set; }
        public DateTime? FinishedAt { get; set; }
        public ContractStatusEnum Status { get; private set; }

        public void Cancel()
        {
            if (Status == ContractStatusEnum.Created || Status == ContractStatusEnum.InProgress)
                Status = ContractStatusEnum.Cancelled;
        }

        public void Start()
        {
            if (Status == ContractStatusEnum.Created)
            {
                Status = ContractStatusEnum.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Finish()
        {
            Status = ContractStatusEnum.Finished;
            FinishedAt = DateTime.Now;
        }
    }
}
