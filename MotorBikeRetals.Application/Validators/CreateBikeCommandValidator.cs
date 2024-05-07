using FluentValidation;
using MotorBikeRetals.Application.Commands.CreateBike;
using MotorBikeRetals.Core.Repositories;
using System.Linq;

namespace MotorBikeRetals.Application.Validators
{
    public class CreateBikeCommandValidator : AbstractValidator<CreateBikeCommand>
    {
        private readonly IBikeRepository _repository;

        public CreateBikeCommandValidator(IBikeRepository repository)
        {
            _repository = repository;

            RuleFor(p => p.Year)
                .NotEmpty()
                .WithMessage("The Year field is required!");

            RuleFor(p => p.Model)
                .NotEmpty()
                .WithMessage("The Model field is required!");

            RuleFor(p => p.Plate)
                .NotEmpty()
                .WithMessage("The Motorcycle license plate field is required!");

            RuleFor(x => x.Plate).Must(plate => {
                    return !_repository.GetAllAsync().Result.Any(r => r.Plate.Equals(plate)); 
                })
                .WithMessage("Motorcycle license plate already registered!");
        }
    }
}
