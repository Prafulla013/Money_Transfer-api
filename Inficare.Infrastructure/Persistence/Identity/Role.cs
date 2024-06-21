using Inficare.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Inficare.Infrastructure.Persistence.Identity
{
    public class Role : IdentityRole<int>, IRole
    {
        public virtual Role ParentRef { get; set; }
    }
}
