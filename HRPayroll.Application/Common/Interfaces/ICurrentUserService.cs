namespace HRPayroll.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        Guid CompanyId { get; }
        bool IsAuthenticated { get; }
        IEnumerable<string> Permissions { get; }
    }
}
