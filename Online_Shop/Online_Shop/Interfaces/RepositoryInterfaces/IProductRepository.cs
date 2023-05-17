using Online_Shop.Models;

namespace Online_Shop.Interfaces.RepositoryInterfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product UpdateProduct(int id, Product product);
        bool DeleteProduct(int id);
        Product CreateProduct(Product product);
    }
}
