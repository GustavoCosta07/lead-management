using FluentValidation;
using FluentValidation.Results;
using MyApp.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Validators
{
    public class AcceptLeadCommandValidator : AbstractValidator<AcceptLeadCommand>
    {
        public AcceptLeadCommandValidator()
        {
            RuleFor(x => x.LeadId)
                .NotEmpty()
                .WithMessage("LeadId é obrigatório.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.FullName))
                .WithMessage("FullName não pode ser vazio.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("PhoneNumber não pode ser vazio.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email inválido.");
        }

        public async Task<ValidationResult> ValidateIncrementalAsync(AcceptLeadCommand command, CancellationToken cancellationToken)
        {
            var result = new ValidationResult();

            if (!string.IsNullOrEmpty(command.FullName))
            {
                var fullNameValidation = await this.ValidateAsync(new ValidationContext<AcceptLeadCommand>(command), cancellationToken);
                result.Errors.AddRange(fullNameValidation.Errors);
            }

            if (!string.IsNullOrEmpty(command.PhoneNumber))
            {
                var phoneNumberValidation = await this.ValidateAsync(new ValidationContext<AcceptLeadCommand>(command), cancellationToken);
                result.Errors.AddRange(phoneNumberValidation.Errors);
            }

            if (!string.IsNullOrEmpty(command.Email))
            {
                var emailValidation = await this.ValidateAsync(new ValidationContext<AcceptLeadCommand>(command), cancellationToken);
                result.Errors.AddRange(emailValidation.Errors);
            }

            return result;
        }
    }
}