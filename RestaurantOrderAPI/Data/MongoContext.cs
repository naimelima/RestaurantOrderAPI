using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestaurantOrderAPI.Models;
using RestaurantOrderAPI.Repositories;

namespace RestaurantOrderAPI.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Order> Orders =>
            _database.GetCollection<Order>("Orders");

        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("Users");
    }
}
