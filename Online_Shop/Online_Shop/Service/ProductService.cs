using AutoMapper;
using Online_Shop.Dto;
using Online_Shop.Exceptions;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Interfaces.ServiceInterfaces;
using Online_Shop.Models;
using Online_Shop.Repository;
using System.Text;

namespace Online_Shop.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProductService(IProductRepository productRepository, IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ProductDto> CreateProduct(int id, CreateProductDto productDto)
        {
            if (String.IsNullOrEmpty(productDto.Name) || String.IsNullOrEmpty(productDto.Amount.ToString()) ||
                String.IsNullOrEmpty(productDto.Price.ToString()) || String.IsNullOrEmpty(productDto.Description))
                throw new BadRequestException($"You must fill in all fields for adding product!");

            if(productDto.Price < 1 || productDto.Amount < 1)
                throw new BadRequestException($"Invalid field values!");

            List<User> users = await _userRepository.GetAll();
            User salesman = users.Where(s => s.Id == id).FirstOrDefault();

            if (salesman == null)
                throw new NotFoundException($"Salesman with ID: {id} doesn't exist.");

            Product newProduct = _mapper.Map<Product>(productDto);
            newProduct.Image = Encoding.ASCII.GetBytes(productDto.Image);
            newProduct.Deleted = false;
            newProduct.User = salesman;
            newProduct.UserId = id;

            ProductDto dto = _mapper.Map<ProductDto>(await _productRepository.CreateProduct(newProduct));
            dto.Image = Encoding.Default.GetString(newProduct.Image);
            return dto;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            List<Product> products = await _productRepository.GetAllProducts();
            Product p = products.Where(p => p.Id == id).FirstOrDefault();
            if (p == null)
                throw new NotFoundException($"Product with ID: {id} doesn't exist.");
            return await _productRepository.DeleteProduct(id);
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            List<Product> products = await _productRepository.GetAllProducts();
            if (products == null)
                throw new NotFoundException($"There are no products!");
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            Product p = await _productRepository.GetProductById(id);
            if (p == null)
                throw new NotFoundException($"Product with ID: {id} doesn't exist.");
            return _mapper.Map<ProductDto>(p);
        }

        public async Task<ProductDto> UpdateProduct(int id, UpdateProductDto productDto)
        {
            Product p = await _productRepository.GetProductById(id);
            if (p == null)
                throw new NotFoundException($"Product with ID:{id} doesn't exist.");

            if (String.IsNullOrEmpty(productDto.Name) || String.IsNullOrEmpty(productDto.Amount.ToString()) ||
                String.IsNullOrEmpty(productDto.Price.ToString()) || String.IsNullOrEmpty(productDto.Description))
                throw new Exception($"You must fill in all fields for updating product!");

            if (productDto.Price < 1 || productDto.Amount < 1)
                throw new Exception($"Invalid field values!");

            p = _mapper.Map<Product>(productDto);
            p.Image = Encoding.ASCII.GetBytes(productDto.Image);

            ProductDto dto = _mapper.Map<ProductDto>(await _productRepository.UpdateProduct(p));
            dto.Image = Encoding.Default.GetString(p.Image);
            return dto;
        }
    }
}
