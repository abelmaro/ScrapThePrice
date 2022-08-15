using ScrapThePrice.Models;

namespace ScrapThePrice.Services.Interfaces
{
    public interface IProductsService
    {
        List<KeyValuePair<string, IGrouping<string, ProductModel>>> GetProducts(string productName);
    }
}
