using ReadingRadar.Application.Common.Interfaces.Services;

namespace ReadingRadar.Infra.Services;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
