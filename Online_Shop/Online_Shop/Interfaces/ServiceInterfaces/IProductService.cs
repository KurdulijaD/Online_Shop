using Online_Shop.Dto;
using Online_Shop.Models;

namespace Online_Shop.Interfaces.ServiceInterfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(int id);
        Task<ProductDto> UpdateProduct(int id, UpdateProductDto productDto);
        Task<bool> DeleteProduct(int id);
        Task<ProductDto> CreateProduct(int id, CreateProductDto productDto);
    }
}
