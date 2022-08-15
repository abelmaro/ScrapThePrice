using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ScrapThePrice.Services
{
    public class ProductHelperService : IProductHelperService
    {
        public string GetUrl(string productName, string site)
        {
            switch (site)
            {
                case "ML":
                    return "https://listado.mercadolibre.com.ar/" + productName.Replace(" ", "-") + "_OrderId_PRICE_NoIndex_True";
                case "OLX":
                    return "https://www.olx.com.ar/items/q-" + productName.Replace(" ", "-") + "?sorting=asc-price";
                case "FRV":
                    return "https://www.fravega.com/l/?keyword=" + productName.Replace(" ", "+") + "&sorting=TOTAL_SALES_IN_LAST_30_DAYS";
                default:
                    return "";
            }
        }

        public List<ProductModel> GetProductsFromElement(IEnumerable<IWebElement> products, IWebDriver driver, ScrappingSelectors selectors)
        {
            List<ProductModel> result = new List<ProductModel>();

            foreach (var product in products)
            {
                try
                {
                    var imageEl = product.FindElement(By.CssSelector(selectors.ProductImage));
                    new Actions(driver).MoveToElement(imageEl).Perform();

                    result.Add(new ProductModel()
                    {
                        ImageUrl = imageEl.GetAttribute("src"),
                        Name = product.FindElement(By.CssSelector(selectors.ProductName)).Text,
                        Price = product.FindElement(By.CssSelector(selectors.ProductPrice)).Text,
                        ProductUrl = product.FindElement(By.TagName("a")).GetAttribute("href"),
                        Site = selectors.Site,
                    });
                    Thread.Sleep(200);
                }
                catch (Exception)
                {

                    continue;
                }
            }

            return result;
        }

        public List<IWebElement>? GetMatchElements(List<IWebElement>? elements, string productName)
        {
            try
            {
                productName = RemoveDiacritics(productName);

                if (elements.Any(x => RemoveDiacritics(x.Text).Contains(productName)))
                    elements = elements.Where(x => RemoveDiacritics(x.Text).Contains(productName)).ToList();
                else
                    elements = elements.Where(x => Regex.IsMatch(RemoveDiacritics(x.Text), @"" + productName)).ToList();

            }
            catch (Exception)
            {

            }

            return elements;
        }

        private static string RemoveDiacritics(string text)
        {
            text = text.ToLower();
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}