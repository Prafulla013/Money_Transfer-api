using FluentValidation;
using Inficare.Application.Admin.ExchangeRate.Queries;

namespace Inficare.Application.Admin.User.Commands
{
    public class ExchangeRateValidator : AbstractValidator<ListExchangeRateQuery>
    {
        public ExchangeRateValidator()
        {
            RuleFor(r => r.Page)
                .NotEmpty()
                .WithMessage("Page is required.")
                .Must(r => r >= 1)
                .WithMessage("Page must be greater than 1 ");

            RuleFor(r => r.PerPage)
                .NotEmpty()
                .WithMessage("From date is required.")
                .Must(r => r >= 1 && r <= 100)
                .WithMessage("Per page must be in between 1 to 100.");

            RuleFor(r => r.FromDate)
                .NotEmpty()
                .WithMessage("From date is required.")
                .Must(NotBeInTheFuture)
                .WithMessage("From date cannot be in the future.");

            RuleFor(r => r.ToDate)
                .Must((project, endDate) =>
                {
                    return DateTime.Parse(endDate) >= DateTime.Parse(project.FromDate);
                })
                .WithMessage("To date must be greater than From date.")
                .When(w => DateTime.Parse(w.ToDate) > DateTimeOffset.MinValue)
                .NotEmpty()
                .WithMessage("End date is required.")
                .Must(NotBeInTheFuture)
                .WithMessage("To date cannot be in the future.");
        }
        private bool NotBeInTheFuture(string date)
        {
            return DateTime.Parse(date) <= DateTimeOffset.UtcNow.LocalDateTime;
        }
    }
}
