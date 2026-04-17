namespace RestaurantOrderAPI.DTOs
{
    public class OrderResponseDto
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
