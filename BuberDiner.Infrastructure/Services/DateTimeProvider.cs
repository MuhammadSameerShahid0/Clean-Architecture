using BuberDiner.Application.Common.Interfaces.Services;

namespace BuberDiner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

