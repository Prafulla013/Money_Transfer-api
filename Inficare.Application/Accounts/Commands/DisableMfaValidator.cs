using FluentValidation;

namespace Inficare.Application.Accounts.Commands
{
    public class DisableMfaValidator : AbstractValidator<DisableMfaCommand>
    {
        public DisableMfaValidator()
        {
            RuleFor(r => r.ClientUrl)
                .NotEmpty()
                .WithMessage("Client url is required.");
        }
    }
}
