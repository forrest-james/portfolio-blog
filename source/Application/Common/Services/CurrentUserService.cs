using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Common.Services
{
    public class CurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) => UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string UserId { get; }
    }
}