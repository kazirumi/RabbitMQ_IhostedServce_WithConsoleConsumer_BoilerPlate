﻿using RabbitMQandIHostedService.Data;
using RabbitMQandIHostedService.Models;

namespace RabbitMQandIHostedService.Services
{
    public class ProductService:IProductService
    {
        private readonly DBContextClass _dbContext;
        public ProductService(DBContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Product> GetProductList()
        {
            return _dbContext.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            return _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefault();
        }
        public Product AddProduct(Product product)
        {
            var result = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public Product UpdateProduct(Product product)
        {
            var result = _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public bool DeleteProduct(int Id)
        {
            var filteredData = _dbContext.Products.Where(x => x.ProductId == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }
    }
}
