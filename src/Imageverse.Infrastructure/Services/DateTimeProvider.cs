using Imageverse.Application.Common.Interfaces.Services;

namespace Imageverse.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
