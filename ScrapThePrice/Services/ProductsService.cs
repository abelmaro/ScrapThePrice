using ScrapThePrice.Models;
using ScrapThePrice.Services.Interfaces;

namespace ScrapThePrice.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IMLScrappingService _mLScrappingService;
        private readonly IOLXScrappingService _oLXScrappingService;
        private readonly IFravegaScrappingService _fravegaScrappingService;


        public ProductsService(IMLScrappingService mLScrappingService,
            IOLXScrappingService oLXScrappingService,
            IFravegaScrappingService fravegaScrappingService)
        {
            _mLScrappingService = mLScrappingService;
            _oLXScrappingService = oLXScrappingService;
            _fravegaScrappingService = fravegaScrappingService;
        }
        public List<KeyValuePair<string, IGrouping<string, ProductModel>>> GetProducts(string productName)
        {
            var products = new List<ProductModel>();

            var threadList = new List<Thread>() {
                new Thread(() => products.AddRange(_mLScrappingService.GetProducts(productName))),
                new Thread(() => products.AddRange(_oLXScrappingService.GetProducts(productName))),
                new Thread(() => products.AddRange(_fravegaScrappingService.GetProducts(productName)))
            };

            Parallel.ForEach(threadList, thread =>
            {
                thread.Start();
                thread.Join();
            });

            return products.GroupBy(x => x.Site).ToDictionary(x => x.Key, x => x).ToList();
        }
    }
}
