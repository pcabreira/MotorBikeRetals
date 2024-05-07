using FluentValidation;
using MotorBikeRetals.Application.Commands.CreateUser;
using MotorBikeRetals.Core.Repositories;
using System.Linq;
using System.Text.RegularExpressions;

namespace MotorBikeRetals.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _repository;

        public CreateUserCommandValidator(IUserRepository repository)
        {
            _repository = repository;

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("Invalid E-mail!");

            RuleFor(u => u.Password)
                .Must(ValidPassword)
                .WithMessage("Password must contain at least 8 characters, a number, an uppercase letter, a lowercase letter, and a special character!");

            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("The Name field is required!");

            RuleFor(u => u.Details.CNPJ)
                .Must(cnpj => {
                    var result = _repository.GetAllAsync().Result;

                    foreach (var item in result)
                    {
                        if(item.Details != null)
                            return !item.Details.CNPJ.Equals(cnpj);
                    }
                    return true;
                })
                .When(r => r.Role == "biker" && r.Details.CNPJ != null)
                .WithMessage("CNPJ already registered!");

            RuleFor(u => u.Details.NumberCNH)
                .Must(numberCNH => {
                    var result = _repository.GetAllAsync().Result;

                    foreach (var item in result)
                    {
                        if (item.Details != null)
                            return !item.Details.NumberCNH.Equals(numberCNH);
                    }
                    return true;
                })
                .When(r => r.Role == "biker" && r.Details.NumberCNH != null)
                .WithMessage("CNH number already registered!");

            RuleFor(u => u.Details.TypeCNH)
                .Must(x => x.Equals("A") || x.Equals("B") || x.Equals("AB"))
                .When(r => r.Role == "biker" && r.Details.TypeCNH != null)
                .WithMessage("Invalid driver's license type!");
        }

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            return regex.IsMatch(password);
        }
    }
}
