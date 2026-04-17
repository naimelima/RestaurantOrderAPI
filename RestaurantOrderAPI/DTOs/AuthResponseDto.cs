namespace RestaurantOrderAPI.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime Expiration { get; set; }
    }
}
