using Microsoft.AspNetCore.Mvc;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Controllers
{
    
    public class ProductsHandlerController
    {
        private readonly IMLScrappingService _mLScrappingService;
        private readonly IOLXScrappingService _oLXScrappingService;
        private readonly IFravegaScrappingService _fravegaScrappingService;


        public ProductsHandlerController(IMLScrappingService mLScrappingService, 
            IOLXScrappingService oLXScrappingService, 
            IFravegaScrappingService fravegaScrappingService)
        {
            _mLScrappingService = mLScrappingService;
            _oLXScrappingService = oLXScrappingService;
            _fravegaScrappingService = fravegaScrappingService;
        }


        [HttpGet]
        [Route("/api/GetProducts")]
        public List<ProductModel> GetProducts(string productName) {
            var products = new List<ProductModel>();
            var productsML = new List<ProductModel>();
            var productsOLX = new List<ProductModel>();
            var productsFravega = new List<ProductModel>();
            Thread tdML = new Thread(() => productsML = _mLScrappingService.GetProducts(productName));
            Thread tdOLX = new Thread(() => productsOLX = _oLXScrappingService.GetProducts(productName));
            Thread tdFravega = new Thread(() => productsFravega = _fravegaScrappingService.GetProducts(productName));

            tdML.Start();
            tdOLX.Start();
            tdFravega.Start();

            tdML.Join();
            tdOLX.Join();
            tdFravega.Join();

            products.AddRange(productsML);
            products.AddRange(productsOLX);
            products.AddRange(productsFravega);

            return products;
        }
    }
}
