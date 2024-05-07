using MediatR;
using System;

namespace MotorBikeRetals.Application.Commands.StartContract
{
    public class StartContractCommand : IRequest<Unit>
    {
        public StartContractCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
