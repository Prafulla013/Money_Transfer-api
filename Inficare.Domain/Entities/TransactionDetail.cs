namespace Inficare.Domain.Entities
{
    public class TransactionDetail: BaseEntity<int>
    {
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset TransactionDate { get; set; } = DateTimeOffset.UtcNow;
        public string TranscationCurrency { get;set; }
        public decimal TranscationRate { get; set; }
        public string TransferBankName { get; set; }
        public string TransferAccountName { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
