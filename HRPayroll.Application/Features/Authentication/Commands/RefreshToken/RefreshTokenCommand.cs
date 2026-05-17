using MediatR;

namespace HRPayroll.Application.Features.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Result<RefreshTokenResponseDto>>;
    public record RefreshTokenResponseDto(string AccessToken, string RefreshToken, DateTime ExpiresAt);
}
