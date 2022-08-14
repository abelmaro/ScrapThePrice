using Microsoft.AspNetCore.Mvc;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Controllers
{
    
    public class ProductsHandlerController
    {
        private readonly IMLScrappingService _mLScrappingService;
        private readonly IOLXScrappingService _oLXScrappingService;

        public ProductsHandlerController(IMLScrappingService mLScrappingService, IOLXScrappingService oLXScrappingService)
        {
            _mLScrappingService = mLScrappingService;
            _oLXScrappingService = oLXScrappingService;
        }


        [HttpGet]
        [Route("/api/GetProducts")]
        public List<ProductModel> GetProducts(string productName) {
            var productsML = new List<ProductModel>();
            var productsOLX = new List<ProductModel>();
            Thread tdML = new Thread(() => productsML = _mLScrappingService.GetProducts(productName));
            Thread tdOLX = new Thread(() => productsOLX = _oLXScrappingService.GetProducts(productName));

            tdML.Start();
            tdML.Join();

            tdOLX.Start();
            tdOLX.Join();

            productsML.AddRange(productsOLX);

            return productsML;
        }
    }
}
