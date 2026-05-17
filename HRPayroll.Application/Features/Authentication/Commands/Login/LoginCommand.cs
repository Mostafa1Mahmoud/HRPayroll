using MediatR;

namespace HRPayroll.Application.Features.Authentication.Commands.Login
{
    public record LoginCommand(string Email, string Password): IRequest<Result<LoginResponseDto>>;
}
