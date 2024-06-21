using Inficare.Domain.Entities;
using Inficare.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inficare.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : BaseConfiguration<TransactionDetail>
    {
        public override void Configure(EntityTypeBuilder<TransactionDetail> builder)
        {
            base.Configure(builder);

            builder.Property(h => h.DrAmount)
                .HasPrecision(12, 2)
                .IsRequired();

            builder.Property(h => h.CrAmount)
                .HasPrecision(12, 2)
                .IsRequired();

            builder.HasOne(h => h.UserProfile)
               .WithOne()
               .HasForeignKey<TransactionDetail>(h => h.UserId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

            builder.Property(h => h.TranscationCurrency)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(h => h.TranscationRate)
                .HasPrecision(12, 2)
                .IsRequired();
        }
    }
}
