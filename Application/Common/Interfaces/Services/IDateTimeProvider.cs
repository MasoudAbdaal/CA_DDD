namespace Application.Common.Interfaces.Services;

public interface IDateTimeProvider
{
    DateTime UTCNow { get; }
}