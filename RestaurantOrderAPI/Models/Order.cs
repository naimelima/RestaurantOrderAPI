namespace RestaurantOrderAPI.Models;

public class Order
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = "Pendente";
}
