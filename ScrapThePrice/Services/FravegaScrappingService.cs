using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ScrapThePrice.Enums;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ScrapThePrice.Services
{
    public class FravegaScrappingService : IFravegaScrappingService
    {
        private readonly IWebDriverService _driverService;
        private readonly IProductHelperService _productHelperService;


        public FravegaScrappingService(IWebDriverService driverService, IProductHelperService productHelperService)
        {
            _driverService = driverService;
            _productHelperService = productHelperService;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string url = _productHelperService.GetUrl(productName, SitesEnum.Fravega);
            IWebDriver driver = _driverService.StartBrowser(url);

            ScrappingSelectors selectors = new ScrappingSelectors()
            {
                ProductImage = ".sc-3c31b0ed-1.bfwBfK",
                ProductName = ".sc-6321a7c8-0.jIfrVg",
                ProductPrice = ".sc-ad64037f-0.Ojxif",
                ProductUrl = "",
                ProductContainer = ".sc-66043bd1-2.cKRLLh",
                Site = "FRAVEGA",
            };

            var footer = driver.FindElement(By.TagName("footer"));
            new Actions(driver).MoveToElement(footer).Perform();

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0,0,5));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".sc-irlQje.dXbnEg")));
            var modal = driver.FindElement(By.CssSelector(".sc-irlQje.dXbnEg"));
            modal.Click();

            var matchElements = _productHelperService.GetMatchElements(driver.FindElements(By.CssSelector(".sc-66043bd1-2.cKRLLh")).ToList(), productName); //TODO: Remove .ToList()

            var products = _productHelperService.GetProductsFromElement(matchElements.Take(5), driver, selectors);

            return products;
        }
    }
}
