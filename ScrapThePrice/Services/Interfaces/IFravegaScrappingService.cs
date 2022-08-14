using ScrapThePrice.Models;

namespace ScrapThePrice.Services.Interfaces
{
    public interface IFravegaScrappingService
    {
        List<ProductModel> GetProducts(string productName);
    }
}
