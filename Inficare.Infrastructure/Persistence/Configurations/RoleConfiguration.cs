using Inficare.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inficare.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasOne(h => h.ParentRef)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
