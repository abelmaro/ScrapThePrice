using OpenQA.Selenium;
using ScrapThePrice.Models;

namespace ScrapThePrice.Services.Interfaces
{
    public interface IProductHelperService
    {
        string GetUrl(string productName, string site);
        List<ProductModel> GetProductsFromElement(IEnumerable<IWebElement> products, IWebDriver driver, ScrappingSelectors selectors);
        List<IWebElement>? GetMatchElements(List<IWebElement>? elements, string productName);
        List<ProductModel> MatchAndGetProducts(List<IWebElement> products, IWebDriver driver, ScrappingSelectors selectors, string productName);

    }
}
