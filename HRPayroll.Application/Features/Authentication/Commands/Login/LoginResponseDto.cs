namespace HRPayroll.Application.Features.Authentication.Commands.Login
{
    public record LoginResponseDto(
        Guid UserId,
        string Email,
        string FullName,
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt
    );
}
