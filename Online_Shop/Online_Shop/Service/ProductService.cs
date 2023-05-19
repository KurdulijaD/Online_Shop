using AutoMapper;
using Online_Shop.Dto;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Interfaces.ServiceInterfaces;
using Online_Shop.Models;
using System.Text;

namespace Online_Shop.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProductService(IProductRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            Product newProduct = _mapper.Map<Product>(productDto);
            newProduct.Image = Encoding.ASCII.GetBytes(productDto.Image);

            if (String.IsNullOrEmpty(newProduct.Name) || String.IsNullOrEmpty(newProduct.Amount.ToString()) ||
                String.IsNullOrEmpty(newProduct.Price.ToString()) || String.IsNullOrEmpty(newProduct.Description))
                throw new Exception($"You must fill in all fields for adding product!");

            if(newProduct.Price < 1 || newProduct.Amount < 1)
                throw new Exception($"Invalid field values!");

            newProduct.Deleted = false;

            ProductDto dto = _mapper.Map<ProductDto>(await _repository.CreateProduct(newProduct));
            dto.Image = Encoding.Default.GetString(newProduct.Image);
            return dto;
        }

        public async Task<ProductDto> DeleteProduct(int id)
        {
            Product p = await _repository.DeleteProduct(id);
            if (p == null)
                throw new Exception($"Product with ID: {id} doesn't exist.");
            return _mapper.Map<ProductDto>(p);
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            List<Product> products = await _repository.GetAllProducts();
            if (products == null)
                throw new Exception($"There are no products!");
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            Product p = await _repository.GetProductById(id);
            if (p == null)
                throw new Exception($"Product with ID: {id} doesn't exist.");
            return _mapper.Map<ProductDto>(p);
        }

        public async Task<ProductDto> UpdateProduct(ProductDto productDto)
        {
            Product newProduct = _mapper.Map<Product>(productDto);
            newProduct.Image = Encoding.ASCII.GetBytes(productDto.Image);

            if (String.IsNullOrEmpty(newProduct.Name) || String.IsNullOrEmpty(newProduct.Amount.ToString()) ||
                String.IsNullOrEmpty(newProduct.Price.ToString()) || String.IsNullOrEmpty(newProduct.Description))
                throw new Exception($"You must fill in all fields for adding product!");

            if (newProduct.Price < 1 || newProduct.Amount < 1)
                throw new Exception($"Invalid field values!");

            ProductDto dto = _mapper.Map<ProductDto>(await _repository.UpdateProduct(newProduct));
            dto.Image = Encoding.Default.GetString(newProduct.Image);
            return dto;
        }
    }
}
