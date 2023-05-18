﻿using Online_Shop.Dto;

namespace Online_Shop.Interfaces.ServiceInterfaces
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
        ProductDto CreateProduct(ProductDto product);
        bool DeleteProduct(int productId);
        ProductDto UpdateProduct(ProductDto product);
    }
}