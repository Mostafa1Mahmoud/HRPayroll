using FluentValidation;
using System;
namespace HRPayroll.Application.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator:  AbstractValidator<ChangePasswordCommand>  
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("New password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("New password must contain at least one digit.")
                .Matches(@"[\W]").WithMessage("New password must contain at least one special character.")
                .Matches(@"^(?!.*\s).*$").WithMessage("New password must not contain whitespace.")
                .Matches(@"^(?!.*\bpassword\b).*$").WithMessage("New password must not contain the word 'password'.")
                .Matches(@"^(?!.*\b1234\b).*$").WithMessage("New password must not contain common sequences like '1234'.")
                .Matches(@"^(?!.*\bqwerty\b).*$").WithMessage("New password must not contain common sequences like 'qwerty'.")
                .Matches(@"^(?!.*\badmin\b).*$").WithMessage("New password must not contain common sequences like 'admin'.")
                .Matches(@"^(?!.*\buser\b).*$").WithMessage("New password must not contain common sequences like 'user'.")
                .Matches(@"^(?!.*\babc\b).*$").WithMessage("New password must not contain common sequences like 'abc'.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm new password is required.")
                .Equal(x => x.NewPassword).WithMessage("Confirm new password must match the new password.");
        }
    }
}
