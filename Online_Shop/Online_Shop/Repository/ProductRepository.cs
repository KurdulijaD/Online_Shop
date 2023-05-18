﻿using Online_Shop.Data;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Models;

namespace Online_Shop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public Product CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public bool DeleteProduct(int id)
        {
            Product product = _context.Products.Find((int)id);
            product.Deleted = true;
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = _context.Products.Find((int)id);
            return product;
        }

        public Product UpdateProduct(int id, Product product)
        {
            Product temp = _context.Products.Find((int)id);

            temp.Name = product.Name;
            temp.Price = product.Price;
            temp.Description = product.Description;
            temp.Amount = product.Amount;
            temp.Image = product.Image;

            _context.SaveChanges();
            return temp;
        }
    }
}