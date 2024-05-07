using MediatR;
using System;

namespace DevFreela.Application.Commands.DeleteBike
{
    public class DeleteBikeCommand : IRequest<Unit>
    {
        public DeleteBikeCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
