using FluentValidation;

namespace Inficare.Application.Admin.User.Commands
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(300)
                .WithMessage("Maximum character limit is 300.");

            RuleFor(r => r.MiddleName)
                .MaximumLength(300)
                .WithMessage("Maximum character limit is 300.")
                .When(w => !string.IsNullOrEmpty(w.MiddleName));

            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(300)
                .WithMessage("Maximum character limit is 300.");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$")
                .WithMessage("Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.");
        }
    }
}
