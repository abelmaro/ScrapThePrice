using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ScrapThePrice.Services
{
    public class OLXScrappingService : IOLXScrappingService
    {
        private readonly IWebDriverService _driverService;

        public OLXScrappingService(IWebDriverService driverService)
        {
            _driverService = driverService;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string url = GetOLXUrl(productName);
            IWebDriver driver = _driverService.StartBrowser(url);

            var footer = driver.FindElement(By.TagName("footer"));
            new Actions(driver).MoveToElement(footer).Perform();

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0,0,5));


            var matchElements = driver.FindElements(By.CssSelector(".EIR5N")).ToList(); //TODO: Remove .ToList()
            try
            {
                if (matchElements.Any(x => x.Text.ToLower().Contains(productName.ToLower())))
                    matchElements = matchElements.Where(x => x.Text.ToLower().Contains(productName.ToLower())).ToList();
                else
                    matchElements = matchElements.Where(x => Regex.IsMatch(x.Text.ToLower(), @"" + productName.ToLower())).ToList();

            }
            catch (Exception e)
            {
                matchElements = matchElements;
            }

            var products = GetProductsFromElement(matchElements.Take(5), wait);

            return products;
        }

        private List<ProductModel> GetProductsFromElement(IEnumerable<IWebElement> products, WebDriverWait wait)
        {

            List<ProductModel> result = new List<ProductModel>();

            foreach (var product in products)
            {
                try
                {
                    result.Add(new ProductModel()
                    {
                        ImageUrl = product.FindElement(By.CssSelector("._2grx4")).FindElement(By.TagName("img")).GetAttribute("src"),
                        Name = product.FindElement(By.CssSelector("._2tW1I")).Text,
                        Price = product.FindElement(By.CssSelector("._89yzn")).Text,
                        ProductUrl = product.FindElement(By.TagName("a")).GetAttribute("href"),
                    });
                }
                catch (Exception)
                {
                    Console.WriteLine("This product will be ignored: {0}.", product);
                    continue;
                }

            }

            return result;
        }

        private string GetOLXUrl(string productName)
        {
            return "https://www.olx.com.ar/items/q-" + productName.Replace(" ", "-") + "?sorting=asc-price";
        }
    }
}
