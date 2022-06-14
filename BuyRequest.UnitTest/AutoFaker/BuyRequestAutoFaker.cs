using Bogus;
using BuyRequestDomain.DTO_s;
using BuyRequestDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyRequest.UnitTest.AutoFaker
{
    public class BuyRequestAutoFaker
    {
        public static BuyRequestDTO GenerateBuyRequestDTO()
        {
            Faker<ProductRequestDTO> ProductRequest = new Faker<ProductRequestDTO>()
               .RuleFor(x => x.Id, Guid.NewGuid())
               .RuleFor(x => x.ProductDescription, x => x.Random.String(1, 50))
               .RuleFor(x => x.ProductCategory, Category.Physical)
               .RuleFor(x => x.Quantity, x => x.Random.Int(1, 12))
               .RuleFor(x => x.Value, x => x.Random.Decimal(1, 100));

            BuyRequestDTO BuyRequest = new Faker<BuyRequestDTO>(locale: "pt_PT")
                  .RuleFor(x => x.Code, x => x.Random.Number(1, 12))
                  .RuleFor(x => x.Date, DateTime.Now)
                  .RuleFor(x => x.DeliveryDate, DateTime.Now)
                  .RuleFor(x => x.Client, Guid.NewGuid())
                  .RuleFor(x => x.ClientDescription, x => x.Random.String(1, 50))
                  .RuleFor(x => x.ClientEmail, x => x.Person.Email)
                  .RuleFor(x => x.ClientPhone, x => x.Person.Phone)
                  .RuleFor(x => x.Status, x => x.PickRandom<Status>())
                  .RuleFor(x => x.Discount, 0)
                  .RuleFor(x => x.CostValue, 0)
                  .RuleFor(x => x.TotalValue, 0)
                  .RuleFor(x => x.Products, x => ProductRequest.GenerateBetween(1, 2));

            return BuyRequest;
        }


    }
}
