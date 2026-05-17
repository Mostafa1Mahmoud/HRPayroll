using MediatR;

namespace HRPayroll.Application.Features.Authentication.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery : IRequest<Result<CurrentUserDto>>;
}
