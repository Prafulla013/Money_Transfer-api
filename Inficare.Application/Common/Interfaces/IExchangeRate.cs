namespace Inficare.Application.Common.Interfaces
{
    public interface IExchangeRate
    {
        Task<Rate> getRateAsync(string currencyId);
    }
}
