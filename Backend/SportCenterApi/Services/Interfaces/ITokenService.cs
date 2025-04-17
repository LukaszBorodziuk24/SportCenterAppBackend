using SportCenterApi.Entities;

namespace SportCenterApi.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(AppUser user);
    }
}