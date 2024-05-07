using FluentValidation;
using MotorBikeRetals.Application.Commands.CreateContract;

namespace MotorBikeRetals.Application.Validators
{
    public class CreateContractCommandValidator : AbstractValidator<CreateContractCommand>
    {
        public CreateContractCommandValidator()
        {
            RuleFor(c => c.TypeCNHUser)
                .Must(c => c.Equals("A"))
                .WithMessage("Invalid driver's license type!");
        }
    }
}
