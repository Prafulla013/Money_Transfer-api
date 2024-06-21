using Inficare.Application.Common.Interfaces;
using Inficare.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Inficare.Application.Admin.User.Commands
{
    public class DepositBalanceHandler : IRequestHandler<DepositBalanceCommand, int>
    {
        private readonly IInficareDbContext _dbContext;
        public DepositBalanceHandler(IInficareDbContext dbContext,
                                 IIdentityService identityService)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(DepositBalanceCommand request, CancellationToken cancellationToken)
        {
            var dbBalance = new Balance
            {
                UserId = request.UserId,
                Amount = request.Amount,
                AmountCurrency = request.Currency,
                IsActive = true,
                LastUpdatedAt = DateTimeOffset.UtcNow,
                LastUpdatedBy = "SA"
            };
            _dbContext.Balance.Add(dbBalance);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return dbBalance.Id;
        }
    }

    public class DepositBalanceCommand : IRequest<int>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
