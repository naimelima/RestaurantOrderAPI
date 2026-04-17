using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestaurantOrderAPI.Models;
using RestaurantOrderAPI.Repositories;

namespace RestaurantOrderAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {
            var mongoSettings = settings.Value;

            if (mongoSettings == null || string.IsNullOrEmpty(mongoSettings.ConnectionString))
                throw new Exception("MongoSettings não foi configurado corretamente.");

            var client = new MongoClient(mongoSettings.ConnectionString);
            _database = client.GetDatabase(mongoSettings.DatabaseName);
        }

        public IMongoCollection<Order> Orders =>
            _database.GetCollection<Order>("Orders");

        public IMongoCollection<Product> Products =>
            _database.GetCollection<Product>("Products");

        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("Users");
    }
}
