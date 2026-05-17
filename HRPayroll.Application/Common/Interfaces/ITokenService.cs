using HRPayroll.Domain.Entities.Identity;

namespace HRPayroll.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, IEnumerable<string> permissions);
        string GenerateRefreshToken();
        Guid GetUserIdFromExpiredToken(string token);
    }
}
