using MediatR;
using MotorBikeRetals.Core.Entities;
using System.Collections.Generic;

namespace MotorBikeRetals.Application.Queries.GetBikeByPlate
{
    public class GetBikeByPlateQuery : IRequest<List<Bike>>
    {
        public GetBikeByPlateQuery(string plate)
        {
            Plate = plate;
        }

        public string Plate { get; private set; }
    }
}
