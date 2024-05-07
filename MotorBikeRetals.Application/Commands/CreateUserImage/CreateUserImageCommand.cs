using MediatR;
using Microsoft.AspNetCore.Http;

namespace MotorBikeRetals.Application.Commands.CreateUserImage
{
    public class CreateUserImageCommand : IRequest<Unit>
    {
        public string IdUser { get; set; }
        public IFormFile File { get; set; }
    }
}
