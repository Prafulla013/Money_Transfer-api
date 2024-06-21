namespace Inficare.Domain.Entities
{
    public class Balance : BaseEntity<int>
    {
        public int UserId { get; set; }
        public string AmountCurrency { get; set; }
        public decimal Amount { get; set; }
        public virtual UserProfile Profile { get; set; }
    }
}
