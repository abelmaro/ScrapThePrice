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
            catch (Exception e)
            {

                url = url;
            }


            driver.Navigate().GoToUrl(url);
            var footer = driver.FindElement(By.TagName("footer"));
            new Actions(driver).MoveToElement(footer).Perform();

            var matchElements = driver.FindElements(By.CssSelector(".ui-search-layout__item")).ToList(); //TODO: Remove .ToList()
            try
            {
                if (matchElements.Any(x => x.Text.ToLower().Contains(productName.ToLower())))
                    matchElements = matchElements.Where(x => x.Text.ToLower().Contains(productName.ToLower())).ToList();
                else
                    matchElements = matchElements.Where(x => Regex.IsMatch(x.Text.ToLower(), @"" + productName.ToLower())).ToList();

            }
            catch (Exception e)
            {
                throw e;
            }
            
            var products = GetProductsFromElement(matchElements.Take(5));

            return products;
        }

        private string GetMlUrl(string productName)
        {
            return "https://listado.mercadolibre.com.ar/" + productName.Replace(" ", "-") + "_OrderId_PRICE_NoIndex_True";
        }

        private List<ProductModel> GetProductsFromElement(IEnumerable<IWebElement> products)
        {

            List<ProductModel> result = new List<ProductModel>();
            
            foreach (var product in products)
            {
                result.Add(new ProductModel()
                {

                    ImageUrl = product.FindElement(By.CssSelector(".ui-search-result-image__element")).GetAttribute("src"),
                    Name = product.FindElement(By.CssSelector(".ui-search-item__title")).Text,
                    Price = product.FindElement(By.CssSelector(".price-tag-fraction")).Text,
                    ProductUrl = product.FindElement(By.CssSelector(".ui-search-link")).GetAttribute("href"),
                });
            }

            return result;
        }
    }
}