using Inficare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Inficare.Application.Common.Interfaces
{
    public interface IInficareDbContext
    {
        DbSet<UserProfile> Profiles { get; set; }
        DbSet<Balance> Balance { get; set; }
        DbSet<TransactionDetail> TransactionDetail { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
    }
}
