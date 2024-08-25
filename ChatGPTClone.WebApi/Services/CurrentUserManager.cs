using System.Security.Claims;
using ChatGPTClone.Application.Common.Interfaces;

namespace ChatGPTClone.WebApi.Services
{
    public class CurrentUserManager : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor; //This is a service provided by ASP.NET Core to access the current HTTP request.

        public CurrentUserManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public Guid UserId => GetUserId();


        private Guid GetUserId()
        {
            var userId = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue("uid");

            return string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
        }

    }
}
