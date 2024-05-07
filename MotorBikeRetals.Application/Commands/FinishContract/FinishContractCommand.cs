using MediatR;
using System;

namespace MotorBikeRetals.Application.Commands.FinishContract
{
    public class FinishContractCommand : IRequest<Unit>
    {
        public FinishContractCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
