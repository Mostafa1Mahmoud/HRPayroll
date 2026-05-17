using HRPayroll.Application.Common.Interfaces;
using HRPayroll.Application.Common.Models;
using HRPayroll.Domain.Entities.Identity;
using MediatR;

namespace HRPayroll.Application.Features.Authentication.Queries.GetCurrentUser
{
    public class GetCurrentUserHandler: IRequestHandler<GetCurrentUserQuery, Result<CurrentUserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetCurrentUserHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(
                _currentUserService.UserId,
                cancellationToken);

            if(user is null)
                return Result.Failure<CurrentUserDto>("User not found");

            var permissions = user.GetPermissions();

            return Result.Success(new CurrentUserDto(
                user.Id,
                user.Email,
                user.FullName,
                user.CompanyId,
                permissions
            ));
        }
    }
}
