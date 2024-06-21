using Inficare.Infrastructure.Common.Handlers;
using Inficare.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inficare.Api.Controllers.AdminArea
{
    [AdminAccess]
    [Authorize]
    [Route("admin-portal/[controller]")]
    [ApiController]
    public class BaseAdminController : BaseController
    {
        protected string CurrentUserName => HttpContext.User?.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name)?.Value ?? "SA";
        protected string Role => HttpContext.User?.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Role)?.Value;
    }
}
