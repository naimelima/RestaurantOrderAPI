using RestaurantOrderAPI.DTOs;
using RestaurantOrderAPI.Models;

namespace RestaurantOrderAPI.Iterfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        string GenerateToken(User user);
    }
}
