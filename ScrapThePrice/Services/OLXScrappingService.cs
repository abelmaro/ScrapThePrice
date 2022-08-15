using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
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

        public OLXScrappingService(IWebDriverService driverService, IProductHelperService productHelperService)
        {
            _driverService = driverService;
            _productHelperService = productHelperService;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string url = _productHelperService.GetUrl(productName, SitesEnum.OLX);
            IWebDriver driver = _driverService.StartBrowser(url);

            var footer = driver.FindElement(By.TagName("footer"));
            new Actions(driver).MoveToElement(footer).Perform();

            ScrappingSelectors selectors = new ScrappingSelectors()
            {
                ProductContainer = ".EIR5N",
                ProductImage = "._3Kg_w",
                ProductName = "._2tW1I",
                ProductPrice = "._89yzn",
                ProductUrl = ".EIR5N",
                Site = "OLX"
            };

            var matchElements = _productHelperService.GetMatchElements(driver.FindElements(By.CssSelector(selectors.ProductContainer)).ToList(), productName); //TODO: Remove .ToList()

            var products = _productHelperService.GetProductsFromElement(matchElements.Take(5), driver, selectors);

            return products;
        }
    }
}
