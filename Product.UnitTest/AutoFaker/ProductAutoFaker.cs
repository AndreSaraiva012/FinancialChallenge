using Bogus;
using ProductDomain.DTO_s;
using ProductDomain.Entities;

namespace Product.UnitTest.AutoFaker
{
    public class ProductAutoFaker
    {
        public static ProductDTO GeneratProductDTO()
        {
            Faker<ProductDTO> Product = new Faker<ProductDTO>()
           .RuleFor(x => x.Code, x => x.Random.String(1, 50))
           .RuleFor(x => x.Description, x => x.Random.String(1, 50))
           .RuleFor(x => x.ProductCategory, x => x.PickRandom<Category>())
           .RuleFor(x => x.GTIN, x => x.Random.String(1, 50))
           .RuleFor(x => x.QRCode, x => x.Random.String(1, 50));

            return Product;
        }
    }
}
