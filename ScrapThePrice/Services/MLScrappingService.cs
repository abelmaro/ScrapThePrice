using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ScrapThePrice.Services
{
    public class MLScrappingService : IMLScrappingService
    {
        private readonly IWebDriverService _driverService;

        public MLScrappingService(IWebDriverService driverService)
        {
            _driverService = driverService;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string mlUrl = GetMlUrl(productName);
            IWebDriver driver = _driverService.StartBrowser(mlUrl);

            var url = mlUrl;
            try
            {
                url = driver.FindElements(By.CssSelector("a[aria-label='Nuevo']")).First().GetAttribute("href");
            }
            catch (Exception)
            {

                url = url;
            }

            driver.Navigate().GoToUrl(url);

            var matchElements = driver.FindElements(By.CssSelector(".ui-search-layout__item")).ToList(); //TODO: Remove .ToList()
            try
            {
                if (matchElements.Any(x => x.Text.ToLower().Contains(productName.ToLower())))
                    matchElements = matchElements.Where(x => x.Text.ToLower().Contains(productName.ToLower())).ToList();
                else
                    matchElements = matchElements.Where(x => Regex.IsMatch(x.Text.ToLower(), @"" + productName.ToLower())).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            
            var products = GetProductsFromElement(matchElements.Take(5), driver);

            return products;
        }

        private string GetMlUrl(string productName)
        {
            return "https://listado.mercadolibre.com.ar/" + productName.Replace(" ", "-") + "_OrderId_PRICE_NoIndex_True";
        }

        private List<ProductModel> GetProductsFromElement(IEnumerable<IWebElement> products, IWebDriver driver)
        {

            List<ProductModel> result = new List<ProductModel>();
            
            foreach (var product in products)
            {
                var imageEl = product.FindElement(By.CssSelector(".ui-search-result-image__element"));
                new Actions(driver).MoveToElement(imageEl).Perform();
                result.Add(new ProductModel()
                {

                    ImageUrl = imageEl.GetAttribute("src"),
                    Name = product.FindElement(By.CssSelector(".ui-search-item__title")).Text,
                    Price = product.FindElement(By.CssSelector(".price-tag-fraction")).Text,
                    ProductUrl = product.FindElement(By.CssSelector(".ui-search-link")).GetAttribute("href"),
                    Site = "MERCADOLIBRE"
                });
                Thread.Sleep(200);
            }

            return result;
        }
    }
}