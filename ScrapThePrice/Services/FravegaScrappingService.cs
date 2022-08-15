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
    public class FravegaScrappingService : IFravegaScrappingService
    {
        private readonly IWebDriverService _driverService;
        private readonly IProductHelperService _productHelperService;
        private readonly ScrappingSitesConfig _config;

        public FravegaScrappingService(IWebDriverService driverService, IProductHelperService productHelperService, ScrappingSitesConfig config)
        {
            _driverService = driverService;
            _productHelperService = productHelperService;
            _config = config;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string url = _productHelperService.GetUrl(productName, SitesEnum.Fravega);
            IWebDriver driver = _driverService.StartBrowser(url);

            ScrappingSelectors selectors = _config.Fravega;


            var footer = driver.FindElement(By.TagName("footer"));
            new Actions(driver).MoveToElement(footer).Perform();

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0,0,5));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".sc-irlQje.dXbnEg")));
            var modal = driver.FindElement(By.CssSelector(".sc-irlQje.dXbnEg"));
            modal.Click();

            var products = driver.FindElements(By.CssSelector(selectors.ProductContainer)).ToList(); //TODO: Remove .ToList()
            return _productHelperService.MatchAndGetProducts(products, driver, selectors, productName);
        }
    }
}
