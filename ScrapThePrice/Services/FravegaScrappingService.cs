using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ScrapThePrice.Services
{
    public class FravegaScrappingService : IFravegaScrappingService
    {
        private readonly IWebDriverService _driverService;

        public FravegaScrappingService(IWebDriverService driverService)
        {
            _driverService = driverService;
        }

        public List<ProductModel> GetProducts(string productName)
        {
            string url = GetFravgeUrl(productName);
            IWebDriver driver = _driverService.StartBrowser(url);

            var footer = driver.FindElement(By.TagName("footer"));
            new Actions(driver).MoveToElement(footer).Perform();

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0,0,5));


            var matchElements = driver.FindElements(By.CssSelector(".sc-66043bd1-2 cKRLLh")).ToList(); //TODO: Remove .ToList()
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

            var products = GetProductsFromElement(matchElements.Take(5), driver);

            return products;
        }

        private List<ProductModel> GetProductsFromElement(IEnumerable<IWebElement> products, IWebDriver driver)
        {

            List<ProductModel> result = new List<ProductModel>();

            foreach (var product in products)
            {
                try
                {
                    var imageEl = product.FindElement(By.CssSelector(".sc-3c31b0ed-1 bfwBfK")).FindElement(By.TagName("img"));
                    new Actions(driver).MoveToElement(imageEl).Perform();

                    result.Add(new ProductModel()
                    {
                        ImageUrl = imageEl.GetAttribute("src"),
                        Name = product.FindElement(By.CssSelector(".sc-6321a7c8-0 jIfrVg")).Text,
                        Price = product.FindElement(By.CssSelector(".sc-ad64037f-0 Ojxif")).Text,
                        ProductUrl = product.FindElement(By.TagName("a")).GetAttribute("href"),
                        Site = "FRAVEGA"
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

        private string GetFravgeUrl(string productName)
        {
            return "https://www.fravega.com/l/?keyword=" + productName.Replace(" ", "+") + "&sorting=TOTAL_SALES_IN_LAST_30_DAYS";
        }
    }
}
