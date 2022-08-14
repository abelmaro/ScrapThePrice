using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Services
{
    public class WebDriverService : IWebDriverService
    {
        public IWebDriver driver = new ChromeDriver();

        public WebDriverService() {
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0,0,10);
        }

        public IWebDriver StartBrowser(string url) {

            driver.Navigate().GoToUrl(url);
            return driver;
        }
    }
}
