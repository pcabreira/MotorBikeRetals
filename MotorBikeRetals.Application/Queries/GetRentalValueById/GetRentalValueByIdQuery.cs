using MediatR;
using System;

namespace MotorBikeRetals.Application.Queries.GetContractById
{
    public class GetRentalValueByIdQuery : IRequest<decimal>
    {
        public GetRentalValueByIdQuery(Guid id, DateTime returnDate)
        {
            Id = id;
            ReturnDate = returnDate;
        }

        public Guid Id { get; private set; }
        public DateTime ReturnDate { get; private set; }
    }
}
