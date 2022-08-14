using OpenQA.Selenium;

namespace ScrapThePrice.Services.Interfaces
{
    public interface IWebDriverService
    {
        IWebDriver StartBrowser(string url);
    }
}
