using Microsoft.AspNetCore.Mvc;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Controllers
{
    
    public class ProductsHandlerController
    {
        private readonly IProductsService _productsService;
        public ProductsHandlerController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        [HttpGet]
        [Route("/api/GetProducts")]
        public List<KeyValuePair<string, IGrouping<string, ProductModel>>> GetProducts(string productName) {
            return _productsService.GetProducts(productName);
        }
    }
}
