using Inficare.Application.Common.Exceptions;
using Inficare.Application.Common.Interfaces;
using Inficare.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Inficare.Application.Admin.User.Commands
{
    public class SendMoneyHandler : IRequestHandler<SendMoneyCommand, bool>
    {
        private readonly IInficareDbContext _dbContext;
        private readonly IExchangeRate _exchangeRate;
        public SendMoneyHandler(IInficareDbContext dbContext,
                                 IIdentityService identityService,
                                 IExchangeRate exchangeRate)
        {
            _dbContext = dbContext;
            _exchangeRate = exchangeRate;
        }

        public async Task<bool> Handle(SendMoneyCommand request, CancellationToken cancellationToken)
        {
           var balance = await _dbContext.Balance.FirstOrDefaultAsync(fd => fd.UserId == request.UserId,cancellationToken);
            if(balance == null)
            {
                throw new BadRequestException();
            }

            var rates = await _exchangeRate.getRateAsync(request.TranscationCurrency);

            var availableBalance = balance.Amount;

            var transferBalance = Convert.ToInt64(rates.sell) * request.Amount;

            if(availableBalance < transferBalance)
            {
                throw new BadRequestException();
            }

            balance.Amount = availableBalance - transferBalance;
            _dbContext.Balance.Update(balance);

            var dbTranscation = new TransactionDetail
            {
                DrAmount = transferBalance,
                CrAmount = 0,
                UserId = request.UserId,
                TranscationCurrency = request.TranscationCurrency,
                TranscationRate = Convert.ToDecimal(rates.sell),
                TransferBankName = request.TransferBankName,
                TransferAccountName = request.TransferAccountName
            };

            _dbContext.TransactionDetail.Add(dbTranscation);


            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    public class SendMoneyCommand : IRequest<bool>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string TranscationCurrency { get; set; }
        public string TransferBankName { get; set; }
        public string TransferAccountName { get; set; }
    }
}
