using Inficare.Application.Common.Interfaces;
using Inficare.Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Inficare.Application.Admin.User.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IInficareDbContext _dbContext;
        private readonly IIdentityService _identityService;
        public CreateUserHandler(IInficareDbContext dbContext,
                                 IIdentityService identityService)
        {
            _dbContext = dbContext;
            _identityService = identityService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUserProfile = new UserProfile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                IsActive = true,
                LastUpdatedAt = DateTimeOffset.UtcNow,
                LastUpdatedBy = request.CurrentUserEmail
            };

            var result = await _identityService.CreateAgentAsync(dbUserProfile, request.Password, request.Email, request.ClientUrl, cancellationToken);
            return result;
        }
    }

    public class CreateUserCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string ClientUrl { get; set; }
        [JsonIgnore]
        public string CurrentUserEmail { get; set; }
    }
}
