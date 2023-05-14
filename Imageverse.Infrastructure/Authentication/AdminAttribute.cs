using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Infrastructure.Authentication
{
    public class AdminAttribute : TypeFilterAttribute
    {
        public AdminAttribute() : base(typeof(AdminFilter))
        {
        }
    }
}
