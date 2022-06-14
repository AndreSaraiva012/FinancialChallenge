using ProductDomain.Entities;

namespace ProductDomain.DTO_s
{
    public class ProductDTO
    {
        public string? Code { get; set; }
        public string Description { get; set; }
        public Category ProductCategory { get; set; }
        public string GTIN { get; set; }
        public string? QRCode { get; set; }
    }
}
