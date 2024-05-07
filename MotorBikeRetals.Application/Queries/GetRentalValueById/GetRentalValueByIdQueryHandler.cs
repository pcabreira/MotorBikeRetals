using MediatR;
using MongoDB.Driver.Linq;
using MotorBikeRetals.Application.Queries.GetContractById;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Queries.GetRentalValueById
{
    public class GetRentalValueByIdQueryHandler : IRequestHandler<GetRentalValueByIdQuery, decimal>
    {
        private readonly IContractRepository _repository;
        private readonly IPlanRepository _repositoryPlan;
        public GetRentalValueByIdQueryHandler(IContractRepository contractRepository, IPlanRepository repositoryPlan)
        {
            _repository = contractRepository;
            _repositoryPlan = repositoryPlan;
        }

        public async Task<decimal> Handle(GetRentalValueByIdQuery request, CancellationToken cancellationToken)
        {
            var contract = await _repository.GetByIdAsync(request.Id);
            var plan = await _repositoryPlan.GetByIdAsync(contract.IdPlan);
            decimal amountToPay = contract.TotalCost;

            if (contract.ExpectedFinish > request.ReturnDate)
            {
                var daysRemainingCost = contract.ExpectedFinish.Date.Subtract(request.ReturnDate.Date).Days * plan.Cost;

                if (plan.Days == 7)
                     amountToPay = contract.TotalCost + (daysRemainingCost * 20) / 100;
                else if (plan.Days == 15)
                    amountToPay = contract.TotalCost + (daysRemainingCost * 40) / 100;
            }
            else if (contract.ExpectedFinish < request.ReturnDate)
            {
                var additionalDays = request.ReturnDate.Date.Subtract(contract.ExpectedFinish.Date).Days;
                amountToPay = contract.TotalCost + (additionalDays * plan.Cost) + (additionalDays * 50);
            }

            return amountToPay;
        }
    }
}
