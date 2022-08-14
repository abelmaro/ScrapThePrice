using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Services
{
    public class WebDriverService : IWebDriverService
    {
        public WebDriverService() {

        }

        public IWebDriver StartBrowser(string url) {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            return driver;
        }
    }
}
