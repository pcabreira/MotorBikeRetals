using FluentValidation;
using MotorBikeRetals.Application.Commands.UpdateBike;
using MotorBikeRetals.Core.Repositories;
using System.Linq;

namespace MotorBikeRetals.Application.Validators
{
    public class UpdatePlateCommandValidator : AbstractValidator<UpdateBikeCommand>
    {
        private readonly IBikeRepository _repository;

        public UpdatePlateCommandValidator(IBikeRepository repository)
        {
            _repository = repository;

            RuleFor(u => u.Plate)
                .NotEmpty()
                .WithMessage("The Plate field is required!");

            RuleFor(x => x.Plate).Must(plate => {
                    return !_repository.GetAllAsync().Result.Any(r => r.Plate.Equals(plate)); 
                })
                .WithMessage("Motorcycle license plate already registered!");
        }
    }
}
