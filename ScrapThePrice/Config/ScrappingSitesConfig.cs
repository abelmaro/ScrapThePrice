using ScrapThePrice.Models;

namespace ScrapThePrice.Config
{
    public class ScrappingSitesConfig
    {
        public ScrappingSelectors MercadoLibre { get; set; }
        public ScrappingSelectors OLX { get; set; }
        public ScrappingSelectors Fravega { get; set; }
        public int ItemsToGet { get; set; }
        public ScrappingSitesConfig()
        {
            MercadoLibre = new ScrappingSelectors { };
            OLX = new ScrappingSelectors { };
            Fravega = new ScrappingSelectors { };
        }
    }
}
