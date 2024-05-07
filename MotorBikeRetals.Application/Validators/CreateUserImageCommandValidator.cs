using FluentValidation;
using MotorBikeRetals.Application.Commands.CreateUserImage;

namespace MotorBikeRetals.Application.Validators
{
    public class CreateUserImageCommandValidator : AbstractValidator<CreateUserImageCommand>
    {
        public CreateUserImageCommandValidator()
        {
            RuleFor(x => x.File.FileName)
                .Must(a => a.EndsWith(".png") || a.EndsWith(".bmp"))
                .WithMessage("Given file is not a image type");
        }
    }
}
