using HRPayroll.Application.Common.Models;
using MediatR;

namespace HRPayroll.Application.Features.Authentication.Commands.ChangePassword
{
    public record ChangePasswordCommand(
        string CurrentPassword,
        string NewPassword,
        string ConfirmNewPassword
    ) : IRequest<Result>;
}
