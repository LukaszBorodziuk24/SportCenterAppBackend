using System.ComponentModel.DataAnnotations;

namespace SportCenterApi.Models
{
    public class AuthResponseDto
    {
        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
