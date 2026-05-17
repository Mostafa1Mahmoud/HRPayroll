namespace HRPayroll.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
        Task SendLeaveApprovalAsync(string to, string employeeName, string leaveType, string status, CancellationToken cancellationToken = default);
        Task SendWelcomeAsync(string to, string fullName, string temporaryPassword, CancellationToken cancellationToken = default);
    }
}
