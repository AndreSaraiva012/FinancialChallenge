using Infrastructure.BaseClass;

namespace ProductDomain.Entities
{
    public class Product : EntityBase
    {
        public string? Code { get; set; }
        public string Description { get; set; }
        public Category ProductCategory { get; set; }
        public string GTIN { get; set; }
        public string? QRCode { get; set; }
    }
}
