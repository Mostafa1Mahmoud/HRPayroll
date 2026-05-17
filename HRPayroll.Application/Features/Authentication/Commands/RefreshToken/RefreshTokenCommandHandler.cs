using HRPayroll.Application.Common.Interfaces;
using HRPayroll.Application.Common.Models;
using HRPayroll.Domain.Entities.Identity;
using MediatR;

namespace HRPayroll.Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IDateTimeService _dateTimeService;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _dateTimeService = dateTimeService;
        }

        public async Task<Result<RefreshTokenResponseDto>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            Guid userId;

            try
            {
                userId = _tokenService.GetUserIdFromExpiredToken(command.AccessToken);
            }
            catch (Exception ex)
            {
                return Result.Failure<RefreshTokenResponseDto> ("Invalid access token.");
            }

            var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(userId, cancellationToken);

            if(user is null || user.IsDeleted)
            {
                return Result.Failure<RefreshTokenResponseDto>("User Not Found");
            }

            var isValid = user.ValidateRefreshToken(command.RefreshToken);

            if(!isValid)
                return Result.Failure<RefreshTokenResponseDto>("Invalid refresh token.");

            var permissions = user.GetPermissions();

            var newAccessToken = _tokenService.GenerateAccessToken(user, permissions);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var expiresAt = _dateTimeService.UtcNow.AddMinutes(60);

            user.RotateRefreshToken(newRefreshToken, _dateTimeService.UtcNow.AddDays(7));

            return Result.Success(new RefreshTokenResponseDto(
                newAccessToken,
                newRefreshToken,
                expiresAt
            ));
        }
    }
}
