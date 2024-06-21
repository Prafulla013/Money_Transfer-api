using Inficare.Application.Common.Models;

namespace Inficare.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailModel email, CancellationToken cancellationToken);
    }
}
