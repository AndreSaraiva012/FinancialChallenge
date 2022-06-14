using Infrastructure.BaseClass;
using System;

namespace BankRecordDomain.Entities
{
    public class BankRecord : EntityBase
    {
        public Origin? Origin { get; set; }
        public Guid? OriginId { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public decimal Amount { get; set; }
    }
}
