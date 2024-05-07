using MediatR;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.Repositories;
using MotorBikeRetals.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _repository;

        public CreateUserCommandHandler(IAuthService authService, IUserRepository repository)
        {
            _authService = authService;
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new User(request.Name, 
                                passwordHash, 
                                request.Email, 
                                request.Role,
                                request.Details);

            if (request.Role == "biker" && request.Details != null)
            {
                user.Details = new UserDetails(request.Details.CNPJ, 
                                               request.Details.BirthDate, 
                                               request.Details.NumberCNH, 
                                               request.Details.TypeCNH);
            }

            await _repository.AddAsync(user);

            return Unit.Value;
        }
    }
}
