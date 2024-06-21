using Inficare.Domain.Entities;
using Inficare.Domain.Enumerations;
using Inficare.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inficare.Infrastructure.Persistence.Identity
{
    public class User : IdentityUser<int>, IUser, ICreatedEvent, IUpdatedEvent
    {
        public bool IsActive { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public MfaProvider DefaultMfaProvider { get; set; }

        [NotMapped]
        public UserEventActivity EventActivity { get; set; }
        [NotMapped]
        public string Link { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string RoleName { get; set; }

        public virtual UserProfile ProfileRef { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
    }
}
