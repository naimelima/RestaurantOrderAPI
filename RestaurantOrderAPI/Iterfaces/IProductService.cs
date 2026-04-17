using RestaurantOrderAPI.Models;

namespace RestaurantOrderAPI.Iterfaces;

public interface IProductService
{
    List<Product> GetAll();
    Product GetById(string id);
    Product Create(Product product);
    void Update(Product product);
    void Delete(string id);
}
