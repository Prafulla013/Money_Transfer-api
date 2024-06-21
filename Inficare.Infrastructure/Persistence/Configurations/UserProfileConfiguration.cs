using Inficare.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inficare.Infrastructure.Persistence.Configurations
{
    public class UserProfileConfiguration : BaseConfiguration<UserProfile>
    {
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Suffix)
                .HasMaxLength(10)
                .IsRequired(false);

            builder.Property(p => p.FirstName)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(p => p.MiddleName)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(p => p.LastName)
                .HasMaxLength(300)
                .IsRequired(false);
        }
    }
}
