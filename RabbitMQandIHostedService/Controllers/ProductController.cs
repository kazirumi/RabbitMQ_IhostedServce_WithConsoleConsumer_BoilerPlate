using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQandIHostedService.Models;
using RabbitMQandIHostedService.RabbitMQProducerLogic;
using RabbitMQandIHostedService.Services;

namespace RabbitMQandIHostedService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IRabbitMQProducer _rabitMQProducer;
        public ProductController(IProductService _productService, IRabbitMQProducer rabitMQProducer)
        {
            productService = _productService;
            _rabitMQProducer = rabitMQProducer;
        }
        [HttpGet("productlist")]
        public IEnumerable<Product> ProductList()
        {
            var productList = productService.GetProductList();
            return productList;
        }
        [HttpGet("getproductbyid")]
        public Product GetProductById(int Id)
        {
            return productService.GetProductById(Id);
        }
        [HttpPost("addproduct")]
        public IEnumerable<Product> AddProduct(Product[] products)
        {
            //var productData = productService.AddProduct(product);
            //send the inserted product data to the queue and consumer will listening this data from queue
            _rabitMQProducer.SendMessage(products, "product");
            return products;
        }
        [HttpPut("updateproduct")]
        public Product UpdateProduct(Product product)
        {
            return productService.UpdateProduct(product);
        }
        [HttpDelete("deleteproduct")]
        public bool DeleteProduct(int Id)
        {
            return productService.DeleteProduct(Id);
        }
    }
}
