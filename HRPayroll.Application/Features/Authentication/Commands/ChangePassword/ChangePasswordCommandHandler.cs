using HRPayroll.Application.Common.Interfaces;
using HRPayroll.Application.Common.Models;
using HRPayroll.Domain.Entities.Identity;

namespace HRPayroll.Application.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.GetRepository<User>().GetAsync(u =>
                u.Id == _currentUserService.UserId,
                cancellationToken: cancellationToken
            );

            var user = users.FirstOrDefault();

            if(user is null)
                return Result.Failure("User not found.");

            if(!user.VerifyPassword(command.CurrentPassword))
                return Result.Failure("Current password is incorrect.");

            user.UpdatePassword(command.NewPassword);

            _unitOfWork.GetRepository<User>().Update(user);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
