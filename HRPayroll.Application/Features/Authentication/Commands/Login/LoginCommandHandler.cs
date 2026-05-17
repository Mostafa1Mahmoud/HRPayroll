using HRPayroll.Application.Common.Interfaces;
using HRPayroll.Application.Common.Models;
using HRPayroll.Domain.Entities.Identity;
using MediatR;

namespace HRPayroll.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IDateTimeService _dateTimeService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result<LoginResponseDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.GetRepository<User>().GetAsync(u =>
                u.Email == command.Email &&
                !u.IsDeleted,
                cancellationToken
            );

            var user = users.FirstOrDefault();

            if (user is null)
                return Result.Failure<LoginResponseDto>("Invalid email or password.");

            var isValidPassword = user.VerifyPassword(command.Password);

            if(!isValidPassword)
                return Result.Failure<LoginResponseDto>("Invalid email or password.");

            var permissions = user.GetPermissions();

            var accessToken = _tokenService.GenerateAccessToken(user, permissions);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var expiresAt = _dateTimeService.UtcNow.AddMinutes(60);

            return Result.Success(new LoginResponseDto(
                user.Id,
                user.Email,
                user.FullName,
                accessToken,
                refreshToken,
                expiresAt
            ));
        }
    }
}
