using Infrastructure.BaseClass;
using System;

namespace BuyRequestDomain.Entities
{
    public class ProductRequest : EntityBase
    {
        private decimal _total;

        public Guid BuyRequestId { get; set; }
        public virtual BuyRequest BuyRequest { get; set; }
        public Guid ProductId { get; set; } = Guid.NewGuid();
        public string ProductDescription { get; set; }
        public Category ProductCategory { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = Convert.ToDecimal((Quantity * Value).ToString("N2"));
            }
        }
    }
}
