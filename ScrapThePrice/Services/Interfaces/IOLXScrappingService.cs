using ScrapThePrice.Models;

namespace ScrapThePrice.Services.Interfaces
{
    public interface IOLXScrappingService
    {
        List<ProductModel> GetProducts(string productName);
    }
}
