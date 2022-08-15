using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using ScrapThePrice.Config;
using ScrapThePrice.Enums;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ScrapThePrice.Services
{
    public class MLScrappingService : IMLScrappingService
    {
        private readonly IWebDriverService _driverService;
        private readonly IProductHelperService _productHelperService;
        private readonly ScrappingSitesConfig _config;

        public MLScrappingService(IWebDriverService driverService, IProductHelperService productHelperService, ScrappingSitesConfig config)
        {
            _driverService = driverService;
            _productHelperService = productHelperService;
            _config = config;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string mlUrl = _productHelperService.GetUrl(productName, SitesEnum.MercadoLibre);
            IWebDriver driver = _driverService.StartBrowser(mlUrl);

            ScrappingSelectors selectors = _config.MercadoLibre;

            try
            {
                mlUrl = driver.FindElements(By.CssSelector("a[aria-label='Nuevo']")).First().GetAttribute("href");
            }
            catch (Exception)
            {

            }

            driver.Navigate().GoToUrl(mlUrl);
            var products = driver.FindElements(By.CssSelector(selectors.ProductContainer)).ToList(); //TODO: Remove .ToList()
            return _productHelperService.MatchAndGetProducts(products, driver, selectors, productName);
            //var matchElements = _productHelperService.GetMatchElements(, productName); 

            //if (matchElements != null && matchElements.Any())
            //    return _productHelperService.GetProductsFromElement(matchElements.Take(_config.ItemsToGet), driver, selectors);
            //else 
            //    return new List<ProductModel>();
        }
    }
}