using MongoDB.Driver;
using RestaurantOrderAPI.DTOs;
using RestaurantOrderAPI.Models;

namespace RestaurantOrderAPI.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Product> _products;

        public OrderService(MongoDbService mongoDbService)
        {
            _orders = mongoDbService.Orders;
            _products = mongoDbService.Products;
        }

        public async Task<OrderResponseDto> CreateAsync(OrderCreateDto dto)
        {
            var order = new Order
            {
                Items = new List<OrderItem>(),
                CreatedAt = DateTime.UtcNow,
                Status = "Pending"
            };

            foreach (var item in dto.Items)
            {
                var product = await _products
                    .Find(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                    throw new Exception($"Produto {item.ProductId} não encontrado");

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name, // 🔥 salva direto (IMPORTANTE no Mongo)
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            order.Total = order.Items.Sum(i => i.Price * i.Quantity);

            await _orders.InsertOneAsync(order);

            return new OrderResponseDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                Total = order.Total,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        public async Task<List<OrderResponseDto>> GetAllAsync()
        {
            var orders = await _orders.Find(_ => true).ToListAsync();

            return orders.Select(order => new OrderResponseDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                Total = order.Total,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            }).ToList();
        }

        public async Task<OrderResponseDto> UpdateStatusAsync(string id, UpdateOrderStatusDto dto)
        {
            var order = await _orders
                .Find(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception("Pedido não encontrado");

            order.Status = dto.Status;

            await _orders.ReplaceOneAsync(o => o.Id == id, order);

            return new OrderResponseDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                Total = order.Total,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}