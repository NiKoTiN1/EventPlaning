using EventPlanning.Domain.Models;

namespace EventPlanning.BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Account user);
        Task<RefreshToken> GenerateRefreshToken();
        string GetUserIdFromAccessToken(string accessToken);
        bool ValidateRefreshToken(Account user, string refreshToken);
        bool RemoveToken(string refreshToken);
    }
}
