using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Controllers
{
    public class OLXScrappingController
    {
        private readonly IOLXScrappingService _oLXScrappingService;
        public OLXScrappingController(IOLXScrappingService service) {
            _oLXScrappingService = service;
        }

        [HttpGet]
        [Route("/api/GetProductFromOLX")]
        public JsonResult GetProductFromOLX(string productName)
        {
            var result = _oLXScrappingService.GetProducts(productName);

            return new JsonResult(result);
        }

        
    }
}
