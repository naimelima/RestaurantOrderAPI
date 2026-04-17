using RestaurantOrderAPI.Iterfaces;
using RestaurantOrderAPI.Models;
using MongoDB.Driver;

namespace RestaurantOrderAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(MongoDbService mongoDbService)
        {
            _products = mongoDbService.Products;
        }

        public List<Product> GetAll()
        {
            return _products.Find(_ => true).ToList();
        }

        public Product GetById(string id)
        {
            return _products.Find(p => p.Id == id).FirstOrDefault();
        }

        public Product Create(Product product)
        {
            _products.InsertOne(product);
            return product;
        }

        public void Update(Product product)
        {
            _products.ReplaceOne(p => p.Id == product.Id, product);
        }

        public void Delete(string id)
        {
            _products.DeleteOne(p => p.Id == id);
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}