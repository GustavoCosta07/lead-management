using FluentValidation;
using MyApp.Application.Commands;

namespace MyApp.Application.Validators
{
    public class DeclineLeadCommandValidator : AbstractValidator<DeclineLeadCommand>
    {
        public DeclineLeadCommandValidator()
        {
            RuleFor(x => x.LeadId)
                .NotEmpty()
                .WithMessage("LeadId é obrigatório.");
        }
    }
}