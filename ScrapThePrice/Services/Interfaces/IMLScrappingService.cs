using ScrapThePrice.Models;

namespace ScrapThePrice.Services.Interfaces
{
    public interface IMLScrappingService
    {
        List<ProductModel> GetProducts(string productName);
    }
}
