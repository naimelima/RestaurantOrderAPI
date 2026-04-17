using MongoDB.Driver;
using RestaurantOrderAPI.Models;
using RestaurantOrderAPI.Repositories;

namespace RestaurantOrderAPI.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration config)
        {
            var settings = config.GetSection("MongoSettings").Get<MongoDbSettings>();
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<Order> Orders =>
            _database.GetCollection<Order>("Orders");
    }
}
