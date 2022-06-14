using BuyRequestDomain.Entities;
using System;

namespace BuyRequestDomain.DTO_s
{
    public class ProductRequestDTO
    {
        public Guid Id { get; set; }
        public string ProductDescription { get; set; }
        public Category ProductCategory { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal Total => Value * Quantity;

    }
}
