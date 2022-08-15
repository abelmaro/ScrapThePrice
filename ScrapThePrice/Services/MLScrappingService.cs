using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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

        public MLScrappingService(IWebDriverService driverService, IProductHelperService productHelperService)
        {
            _driverService = driverService;
            _productHelperService = productHelperService;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string mlUrl = _productHelperService.GetUrl(productName, SitesEnum.MercadoLibre);
            IWebDriver driver = _driverService.StartBrowser(mlUrl);

            ScrappingSelectors selectors = new ScrappingSelectors()
            {
                ProductImage = ".ui-search-result-image__element",
                ProductName = ".ui-search-item__title",
                ProductUrl = ".ui-search-item__brand-discoverability.ui-search-item__group__element",
                ProductPrice = ".price-tag-fraction",
                ProductContainer = ".ui-search-layout__item",
                Site = "MERCADOLIBRE",
            };

            var url = "";
            try
            {
                url = driver.FindElements(By.CssSelector("a[aria-label='Nuevo']")).First().GetAttribute("href");
            }
            catch (Exception)
            {
                url = mlUrl;
            }

            driver.Navigate().GoToUrl(url);

            var matchElements = _productHelperService.GetMatchElements(driver.FindElements(By.CssSelector(selectors.ProductContainer)).ToList(), productName); //TODO: Remove .ToList()

            var products = _productHelperService.GetProductsFromElement(matchElements.Take(5), driver, selectors);

            return products;
        }
    }
}