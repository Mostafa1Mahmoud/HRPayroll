namespace HRPayroll.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
        DateOnly UtcToday { get; }
    }
}
