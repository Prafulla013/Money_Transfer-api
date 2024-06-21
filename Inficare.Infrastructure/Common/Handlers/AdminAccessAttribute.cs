using Inficare.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Inficare.Domain.Enumerations;
using System.Security.Claims;

namespace Inficare.Infrastructure.Common.Handlers
{
    public class AdminAccessAttribute : TypeFilterAttribute
    {
        public AdminAccessAttribute() : base(typeof(AdminAccessFilter)) { }
    }

    public class AdminAccessFilter : IAsyncAuthorizationFilter
    {
        private readonly IInficareDbContext _dbContext;
        public AdminAccessFilter(IInficareDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //If we need authorization need to add on this attribute
            }
        }
    }
}
