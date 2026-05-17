namespace HRPayroll.Application.Features.Authentication.Queries.GetCurrentUser
{
    public record CurrentUserDto(
        Guid UserId,
        string Email,
        string FullName,
        Guid CompanyId,
        List<string> Permissions
    );
}
