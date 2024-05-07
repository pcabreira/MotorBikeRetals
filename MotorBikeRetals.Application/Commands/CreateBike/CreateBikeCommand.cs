using MediatR;

namespace MotorBikeRetals.Application.Commands.CreateBike
{
    public class CreateBikeCommand : IRequest<Unit>
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}
