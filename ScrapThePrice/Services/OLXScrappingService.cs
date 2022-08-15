using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ScrapThePrice.Config;
using ScrapThePrice.Enums;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ScrapThePrice.Services
{
    public class OLXScrappingService : IOLXScrappingService
    {
        private readonly IWebDriverService _driverService;
        private readonly IProductHelperService _productHelperService;
        private readonly ScrappingSitesConfig _config;

        public OLXScrappingService(IWebDriverService driverService, IProductHelperService productHelperService, ScrappingSitesConfig config)
        {
            _driverService = driverService;
            _productHelperService = productHelperService;
            _config = config;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string url = _productHelperService.GetUrl(productName, SitesEnum.OLX);
            IWebDriver driver = _driverService.StartBrowser(url);

            var footer = driver.FindElement(By.TagName("footer"));
            new Actions(driver).MoveToElement(footer).Perform();

            ScrappingSelectors selectors = _config.OLX;

            var products = driver.FindElements(By.CssSelector(selectors.ProductContainer)).ToList(); //TODO: Remove .ToList()
            return _productHelperService.MatchAndGetProducts(products, driver, selectors, productName);
        }
    }
}
