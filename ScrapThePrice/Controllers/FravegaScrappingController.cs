using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Controllers
{
    public class FravegaScrappingController
    {
        private readonly IFravegaScrappingService _fravegaScrappingService;
        public FravegaScrappingController(IFravegaScrappingService service) {
            _fravegaScrappingService = service;
        }

        [HttpGet]
        [Route("/api/GetProductFromOLX")]
        public JsonResult GetProductFromOLX(string productName)
        {
            var result = _fravegaScrappingService.GetProducts(productName);

            return new JsonResult(result);
        }

        
    }
}
