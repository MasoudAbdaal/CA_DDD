using Application.Common.Interfaces.Services;

namespace Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UTCNow => DateTime.UtcNow;
}