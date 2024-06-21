using Inficare.Domain.Entities;
using Inficare.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inficare.Infrastructure.Persistence.Configurations
{
    public class BalanceConfiguration : BaseConfiguration<Balance>
    {
        public override void Configure(EntityTypeBuilder<Balance> builder)
        {
            base.Configure(builder);

            builder.Property(h => h.AmountCurrency)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(h => h.Amount)
                .HasPrecision(12, 2)
                .IsRequired();

            builder.HasOne(h => h.Profile)
                .WithOne(w => (Balance)w.Balance)
                .HasForeignKey<Balance>(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
