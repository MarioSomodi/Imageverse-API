using Imageverse.Application.Common.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminFilter(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            User? user = await _unitOfWork.GetRepository<IUserRepository>().FindById(UserId.Create(new Guid(userId)));
            if (!user!.IsAdmin)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
