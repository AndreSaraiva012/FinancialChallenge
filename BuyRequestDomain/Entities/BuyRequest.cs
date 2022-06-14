using Infrastructure.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuyRequestDomain.Entities
{
    public class BuyRequest : EntityBase
    {
        private List<ProductRequest> _products = new List<ProductRequest>();
    
        public long Code { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public Guid Client { get; set; }
        public string ClientDescription { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }
        public Status Status { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Sector { get; set; }
        public string Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal ProductValue { get; set; }
        public decimal Discount {  get; set; }
        public decimal CostValue { get; set; }
        public decimal TotalValue { get; set; }
        public List<ProductRequest> Products
        {
            get { return _products; }
            set { _products = value ?? new List<ProductRequest>(); }
        }
    }
}