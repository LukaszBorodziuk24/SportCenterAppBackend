using Microsoft.AspNetCore.Identity;
using SportCenterApi.Models;

namespace SportCenterApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto model);

        Task<AuthResponseDto?> LoginUserAsync(LoginDto dto);


    }
}