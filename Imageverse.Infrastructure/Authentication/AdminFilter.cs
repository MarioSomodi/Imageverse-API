using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Imageverse.Infrastructure.Authentication
{
    internal class AdminFilter : IAsyncAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public AdminFilter(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            User? user = await _userRepository.GetByIdAsync(UserId.Create(new Guid(userId)));
            if (!user!.IsAdmin)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
