using Inficare.Application.Common.Interfaces;
using Inficare.Domain.Enumerations;
using MediatR;

namespace Inficare.Application.Accounts.Queries
{
    public class ListAccountMfaProvidersHandler : IRequestHandler<ListAccountMfaProvidersQuery, MfaProvider[]>
    {
        private readonly IIdentityService _identityService;
        public ListAccountMfaProvidersHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<MfaProvider[]> Handle(ListAccountMfaProvidersQuery request, CancellationToken cancellationToken)
        {
            var providers = await _identityService.ListUserMfaProvidersAsync(request.CurrentUserEmail, cancellationToken);
            return providers;
        }
    }

    public class ListAccountMfaProvidersQuery : IRequest<MfaProvider[]>
    {
        public string CurrentUserEmail { get; set; }
    }
}
