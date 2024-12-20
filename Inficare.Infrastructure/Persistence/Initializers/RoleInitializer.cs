﻿using Inficare.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Inficare.Infrastructure.Persistence.Initializers
{
    public class RoleInitializer : BaseInitializer
    {
        public RoleInitializer(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public const int SUPER_AGENT = 1;
        public const string SUPER_AGENT_NAME = "Super Agent";
        public const int CUSTOMER = 2;
        public const string CUSTOMER_NAME = "Customer";
        public const int AGENT = 3;
        public const string AGENT_NAME = "Agent";
        public void SeedRoles()
        {
            var dbSuperAgent = new Role { Id = SUPER_AGENT, Name = SUPER_AGENT_NAME, ConcurrencyStamp = "647808af-878a-41e5-9d69-5796165214bd", NormalizedName = SUPER_AGENT_NAME.ToUpper() };

            _modelBuilder.Entity<Role>().HasData(dbSuperAgent);
        }
    }
}
