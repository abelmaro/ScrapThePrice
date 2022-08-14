using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Controllers
{
    public class MLScrappingController
    {
        private readonly IMLScrappingService _mLScrappingService;
        public MLScrappingController(IMLScrappingService service) {
            _mLScrappingService = service;
        }

        [HttpGet]
        [Route("/api/GetProductFromML")]
        public JsonResult GetProductFromML(string productName)
        {
            var result = _mLScrappingService.GetProducts(productName);

            return new JsonResult(result);
        }

        
    }
}
